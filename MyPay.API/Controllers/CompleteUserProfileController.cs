using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using log4net;
using MyPay.API.Models;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Repository;

namespace MyPay.API.Controllers
{
    public class CompleteUserProfileController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(CompleteUserProfileController));
        public HttpResponseMessage Post(Req_UserCompleteProfile user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside CompleteUserProfileController" + Environment.NewLine);
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
                    AddUser resGetRecord = new AddUser();

                    

                    if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString().Trim() != "0")
                    {
                        Int64 memId = Convert.ToInt64(user.MemberId);
                        int VendorAPIType = 0;
                        int Type = 0;
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        resGetRecord = new CommonHelpers().CheckUserDetailOld(ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);

                        if (resGetRecord.KycStatusEnum == AddUser.kyc.Verified)
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            cres1.Message = "KYC is already verified. Kindly refresh the app.";
                            cres1.responseMessage = "KYC is already verified. Kindly refresh the app.";
                            cres1.status = false;
                            cres1.ReponseCode = 3;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        
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
                    bool IsCouponUnlocked = false;
                    string msg = RepUser.CompleteUserProfile(ref IsCouponUnlocked, ref resGetRecord, user.StepCompleted, user.MotherName, user.SpouseName, authenticationToken, user.IssueFromDistrictID, user.IssueFromDistrictName, user.IssueFromStateID, user.IssueFromStateName, user.MemberId, user.FirstName, user.MiddleName, user.LastName, user.DateOfBirth, user.Gender, user.MeritalStatus, user.FatherName, user.GrandFatherName, user.Occupation, user.Nationality, user.StateId, user.State, user.DistrictId, user.District, user.MunicipalityId, user.Municipality, user.WardNumber, user.StreetName, user.HouseNumber, user.ProofType, user.IsYourPermanentAndTemporaryAddressSame, user.CurrentStateId, user.CurrentState, user.CurrentDistrictId, user.CurrentDistrict, user.CurrentMunicipalityId, user.CurrentMunicipality, user.CurrentWardNumber, user.CurrentStreetName, user.CurrentHouseNumber, user.IssuedBy, user.DOBType, user.IssueDate, user.ExpiryDate, user.DocumentNumber, user.PlatForm, true, user.DeviceCode);
                    if (msg.ToLower() != "success")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    else
                    {
                        result.Message = msg;
                        result.ReponseCode = 1;
                        result.status = true;
                        result.IsCouponUnlocked = IsCouponUnlocked;
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.OK, result);
                        log.Info($"{System.DateTime.Now.ToString()} CompleteUserProfileController completed" + Environment.NewLine);
                        return response;
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} CompleteUserProfileController {ex.ToString()} " + Environment.NewLine);
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