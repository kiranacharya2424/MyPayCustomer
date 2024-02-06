using log4net;
using MyPay.API.Models;
using MyPay.API.Models.Request;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class AddVotingListController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(AddVotingListController));
        public HttpResponseMessage Post(Req_VotingList user)
        {
            string ApiResponse;
            log.Info($"{System.DateTime.Now.ToString()} inside AddVotingListController" + Environment.NewLine);
            CommonResponse result = new CommonResponse();
            var response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, result);
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
                    string md5hash = Common.CheckHash(user);

                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results.ToLower() != "success")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    string UserInput = getRawPostData().Result;
                    string CommonResult = "";
                    AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
                    AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                    if (user.MemberId > 0)
                    {
                        Int64 memId = Convert.ToInt64(user.MemberId);
                        int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.Voting;
                        int PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? Convert.ToInt32(user.PaymentMode) : 1);
                        resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, PaymentMode, VendorAPIType, memId, false, false, "0", true, user.Mpin);
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
                    string Ipaddress = Common.GetUserIP();
                    string authenticationToken = Request.Headers.Authorization.Parameter;
                    Common.authenticationToken = authenticationToken;


                    string PlatForm = user.PlatForm;
                    string DeviceCode = user.DeviceCode;
                    string msg = String.Empty;
                    string Reference = (new CommonHelpers()).GenerateUniqueId();
                    AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                    int VendorApiType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.Voting;
                    string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorApiType).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                    objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse("", Reference, resGetRecord.MemberId, resGetRecord.FirstName, "", "", UserInput, DeviceCode, PlatForm, VendorApiType);
                    bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                    if (string.IsNullOrEmpty(user.UniqueCustomerId))
                    {
                        user.UniqueCustomerId = user.VotingCandidateUniqueId;
                    }
                    msg = RepUser.VotingListAdd(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, authenticationToken, resGetRecord.MemberId, user.Type, user.VotingCandidateUniqueId, user.VotingPackageID, user.NoofVotes, user.PlatForm, user.DeviceCode, Ipaddress, ref objVendor_API_Requests, VendorApiType);
                    if (msg.ToLower() != "success")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                    }
                    else
                    {
                        result.Details = "Thank You. Your vote has been submitted successfully";
                        result.Message = msg;
                        result.ReponseCode = 1;
                        result.status = true;
                        result.TransactionUniqueId = TransactionID;
                        result.IsCouponUnlocked = IsCouponUnlocked;
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.OK, result);
                        log.Info($"{System.DateTime.Now.ToString()} AddVotingListController completed" + Environment.NewLine);
                        ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
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
                    return response;
                }

            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} AddVotingListController {ex.ToString()} " + Environment.NewLine);
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