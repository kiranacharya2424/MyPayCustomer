using log4net;
using MyPay.API.Models;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Repository;
using System;
using System.Data.Entity.Validation;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class LinkBankResendOTPController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(LinkBankResendOTPController));
        [System.Web.Http.HttpPost]
        public HttpResponseMessage LinkBankResendOTP(Req_LinkBankOTP user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside LinkBankResendOTP" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            var response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
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
                        if (string.IsNullOrEmpty(user.TransactionId))
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Please Enter TransactionId");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        else if (user.MemberId == null || user.MemberId == 0)
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Please Enter MemberId");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }

                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = 0;
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
                        }
                        else
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        if (resuserdetails.Id == 0)
                        {
                            string result1s = "User Not Found";
                            cres = CommonApiMethod.ReturnBadRequestMessage(result1s);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }

                        GetVerifyLinkBank res = RepNps.ResendOtp(user.TransactionId);

                        if (res.code == "0")
                        {
                            response = Request.CreateResponse<GetVerifyLinkBank>(System.Net.HttpStatusCode.Created, res);
                            log.Info($"{System.DateTime.Now.ToString()} LinkBankResendOTP completed" + Environment.NewLine);
                            return response;
                        }
                        else
                        {

                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<GetVerifyLinkBank>(System.Net.HttpStatusCode.BadRequest, res);
                            return response;
                        }
                    }
                }
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
                log.Error($"{System.DateTime.Now.ToString()} LinkBankResendOTP {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}