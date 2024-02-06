using log4net;
using MyPay.API.Models;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class TransferByPhoneController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(TransferByPhoneController));

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/transferbyphone")]
        public HttpResponseMessage Post(Req_TransferByPhone user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside transferbyphone" + Environment.NewLine);
            string ApiResponse = string.Empty;
            Res_TransferByPhone result = new Res_TransferByPhone();
            var response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, result);

            var userInput = getRawPostData().Result;

            try
            {
                if (Request.Headers.Authorization == null)
                {
                    string results = "Un-Authorized Request";
                    CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(results);
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else
                {


                    string md5hash = Common.getHashMD5(userInput); //Common.CheckHash(user);

                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results.ToLower() != "success")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    user.Mpin = user.Pin;
                    user.Pin = Common.Decryption(user.Pin);
                   // string UserInput = getRawPostData().Result;
                    string CommonResult = "";
                    AddUserLoginWithPin resUser = new AddUserLoginWithPin();
                    AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                    if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                    {
                        Int64 memId = Convert.ToInt64(user.MemberId);
                        int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.Transer_by_phone;
                        int Type = (int)WalletTransactions.WalletTypes.Wallet;
                        user.UniqueCustomerId = user.RecipientPhone;
                        resUser = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, user.Referenceno, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, true, user.Amount, true, user.Mpin, "", false, true);
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

                    string authenticationToken = Request.Headers.Authorization.Parameter;
                    Common.authenticationToken = authenticationToken;
                    int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.Transer_by_phone;
                    //user.Referenceno = new CommonHelpers().GenerateUniqueId();
                    AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                    string TransactionUniqueID = string.Empty;
                    string SenderTransactionUniqueID = string.Empty;
                    objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse("", user.Referenceno, resUser.MemberId, resUser.FirstName + " " + resUser.LastName, string.Empty, authenticationToken, userInput, user.DeviceCode, user.PlatForm, VendorApiType, TransactionUniqueID, "", "", "", objVendor_API_Requests.Id);

                    string msg = RepTransactions.TransferByPhone(user.CouponCode, ref TransactionUniqueID, ref SenderTransactionUniqueID, resUser, user.UniqueCustomerId, user.MemberId, user.RecipientPhone, user.Amount, user.Pin, user.Remarks,
                        user.Referenceno, user.PlatForm, user.DeviceCode, true, userInput, authenticationToken, ref objVendor_API_Requests, VendorApiType);
                    if (msg.ToLower() != "success")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                        saveAPIResponse(ApiResponse, ref objVendor_API_Requests);
                        return response;
                    }
                    else
                    {
                        result.Message = "Money transferred successfully";
                        result.ReponseCode = 1;
                        result.status = true;
                        result.TransactionUniqueId = SenderTransactionUniqueID;
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.OK, result);
                        ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        saveAPIResponse(ApiResponse, ref objVendor_API_Requests);
                        log.Info($"{System.DateTime.Now.ToString()} transferbyphone completed" + Environment.NewLine);
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} transferbyphone {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        private void saveAPIResponse(string ApiResponse, ref AddVendor_API_Requests objVendor_API_Requests)
        {
            if (objVendor_API_Requests.Id > 0)
            {

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

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/request-transfer-byphone")]
        public HttpResponseMessage RequestTransferByPhone(Req_TransferByPhone user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside request-transfer-byphone" + Environment.NewLine);
            CommonResponse result = new CommonResponse();
            var response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, result);

            var userInput = getRawPostData().Result;

            try
            {
                if (Request.Headers.Authorization == null)
                {
                    string results = "Un-Authorized Request";
                    CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(results);
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else
                {


                    string md5hash = Common.getHashMD5(userInput); //Common.CheckHash(user);

                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results.ToLower() != "success")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    if (user.PlatForm.ToLower() != "android")
                    {
                        user.Pin = Common.Decryption(user.Pin);
                    }
                    string CommonResult = "";
                    AddUserLoginWithPin resUser = new AddUserLoginWithPin();
                    AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                    if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                    {
                        Int64 memId = Convert.ToInt64(user.MemberId);
                        int VendorAPIType = 0;
                        int Type = 0;
                        resUser = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
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
                    string authenticationToken = Request.Headers.Authorization.Parameter;
                    Common.authenticationToken = authenticationToken;
                    user.Referenceno = new CommonHelpers().GenerateUniqueId();
                    string msg = RepTransactions.Request_TransferByPhone(user.MemberId, user.RecipientPhone, user.Amount, user.Pin, user.Remarks, user.Referenceno, user.PlatForm, user.DeviceCode, true, authenticationToken);
                    if (msg.ToLower() != "success")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    else
                    {
                        result.Message = "Your payment request has been sent successfully.";
                        result.ReponseCode = 1;
                        result.status = true;
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.OK, result);
                        log.Info($"{System.DateTime.Now.ToString()} request-transfer-byphone completed" + Environment.NewLine);
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} request-transfer-byphone {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/get-transfer-byphone")]
        public HttpResponseMessage GetTransferByPhone(Req_TransferByPhone_Get user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside get-transfer-byphone" + Environment.NewLine);
            Res_RequestFund result = new Res_RequestFund();
            var response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, result);

            var userInput = getRawPostData().Result;

            try
            {
                if (Request.Headers.Authorization == null)
                {
                    string results = "Un-Authorized Request";
                    CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(results);
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else
                {
                    string md5hash = Common.getHashMD5(userInput); //Common.CheckHash(user);

                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results.ToLower() != "success")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    string CommonResult = "";
                    AddUserLoginWithPin resUser = new AddUserLoginWithPin();
                    AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                    if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                    {
                        Int64 memId = Convert.ToInt64(user.MemberId);
                        int VendorAPIType = 0;
                        int Type = 0;
                        resUser = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
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
                    List<AddRequestFund> objAddRequestFund = new List<AddRequestFund>();
                    string msg = RepTransactions.Get_TransferByPhone(resUser, user.Take.ToString(), user.Skip.ToString(), user.MemberId, user.PlatForm, user.DeviceCode, true, ref objAddRequestFund);
                    if (msg.ToLower() != "success")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    else
                    {
                        result.RequestFundList = objAddRequestFund;
                        result.Message = "success";
                        result.ReponseCode = 1;
                        result.status = true;
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.OK, result);
                        log.Info($"{System.DateTime.Now.ToString()} get-transfer-byphone completed" + Environment.NewLine);
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} get-transfer-byphone {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/transfer-byphone-reject")]
        public HttpResponseMessage TransferByPhoneReject(Req_TransferByPhone_Get user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside transfer-byphone-reject" + Environment.NewLine);
            CommonResponse result = new CommonResponse();
            var response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, result);

            var userInput = getRawPostData().Result;

            try
            {
                if (Request.Headers.Authorization == null)
                {
                    string results = "Un-Authorized Request";
                    CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(results);
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else
                {
                    string md5hash = Common.getHashMD5(userInput); //Common.CheckHash(user);

                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results.ToLower() != "success")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    string CommonResult = "";
                    AddUserLoginWithPin resUser = new AddUserLoginWithPin();
                    AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                    if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                    {
                        Int64 memId = Convert.ToInt64(user.MemberId);
                        int VendorAPIType = 0;
                        int Type = 0;
                        resUser = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
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
                    string authenticationToken = Request.Headers.Authorization.Parameter;
                    Common.authenticationToken = authenticationToken;
                    string msg = RepTransactions.TransferByPhoneRejectRequest(user.MemberId.ToString(), user.UniqueCustomerId, user.PlatForm, user.DeviceCode, true, authenticationToken);
                    if (msg.ToLower() != "success")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    else
                    {
                        result.Message = "Your payment request has been rejected successfully.";
                        result.ReponseCode = 1;
                        result.status = true;
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.OK, result);
                        log.Info($"{System.DateTime.Now.ToString()} transfer-byphone-reject completed" + Environment.NewLine);
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} transfer-byphone-reject {ex.ToString()} " + Environment.NewLine);
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