using log4net;
using MyPay.API.Models;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Repository;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class SetPinController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(SetPinController));

        public HttpResponseMessage Post(Req_SetPin user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside SetPinController" + Environment.NewLine);
            Res_UserDetail result = new Res_UserDetail();
            var response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, result);

            string userInput = getRawPostData().Result;
            string md5hash = Common.getHashMD5(userInput);
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
                   // string md5hash = Common.CheckHash(user);

                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results.ToLower() != "success")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    string CommonResult = "";
                    AddUserLoginWithPin res = new AddUserLoginWithPin();
                    AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                    if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                    {
                        Int64 memId = Convert.ToInt64(user.MemberId);
                        int VendorAPIType = 0;
                        int Type = 0;
                        res = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
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
                    string msg = RepUser.SetPin(ref res, authenticationToken, user.Pin, user.MemberId, user.Otp, user.IsForget, true, user.PlatForm, user.DeviceCode);
                    if (msg.ToLower() != "success")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    else
                    {
                        if (res != null && res.Id != 0)
                        {
                            result.Name = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                            result.EmailId = res.Email;
                            result.ContactNumber = res.ContactNumber;
                            result.PhoneExt = res.PhoneExtension;
                            result.Gender = res.Gender;
                            result.IsKycVerified = res.IsKYCApproved;
                            result.MemberId = res.MemberId;
                            result.IsDetailUpdated = res.FirstName != "" ? true : false;
                            result.IsPasswordCreated = res.Password != "" ? true : false;
                            result.IsPhoneVerified = res.IsPhoneVerified;
                            result.IsEmailVerified = res.IsEmailVerified;
                            result.IsPinCreated = res.Pin == "" ? false : true;
                            result.TotalAmount = Convert.ToDecimal(res.TotalAmount).ToString("f2");
                            result.Value = Common.EncryptString(res.MemberId.ToString());
                            result.RefCode = res.RefCode;
                            result.Message = msg;
                            result.ReponseCode = 1;
                            result.status = true;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_UserDetail>(System.Net.HttpStatusCode.OK, result);
                            log.Info($"{System.DateTime.Now.ToString()} SetPinController completed" + Environment.NewLine);
                            return response;
                        }
                        else
                        {
                            CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage("User Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} SetPinController {ex.ToString()} " + Environment.NewLine);
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