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
    public class LinkBankVerifyAccountController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(LinkBankVerifyAccountController));
        [System.Web.Http.HttpPost]
        public HttpResponseMessage LinkBankVerifyAccount(Req_LinkBankVerify user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside LinkBankVerifyAccount" + Environment.NewLine);
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
                        if (string.IsNullOrEmpty(user.BankCode))
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Please Select Bank");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        else if (string.IsNullOrEmpty(user.OTPCode))
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Please Enter OTP");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        else if (string.IsNullOrEmpty(user.TransactionId))
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Please Enter TransactionId");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        else if (string.IsNullOrEmpty(user.BankCode))
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Please Enter Bank Code");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        else if (string.IsNullOrEmpty(user.AccountNumber))
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Please Enter Account Number");
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

                        GetVerifyLinkBank res = RepNps.VerifyAccount(user.OTPCode, user.BankCode, user.TransactionId);

                        if (res.code == "0" && res.message == "Success")
                        {
                            string authenticationToken = Request.Headers.Authorization.Parameter;
                            Common.authenticationToken = authenticationToken;
                            string msg = RepUser.AddUserBankDetail(user.LogoUrl, authenticationToken, resuserdetails, resuserdetails.FirstName + " " + resuserdetails.LastName, user.BankCode, user.BankName, res.data.Token, resuserdetails.Email, user.AccountNumber, user.PlatForm, true, user.DeviceCode);
                            if (msg.ToLower() == "success")
                            {
                                response = Request.CreateResponse<GetVerifyLinkBank>(System.Net.HttpStatusCode.Created, res);
                                log.Info($"{System.DateTime.Now.ToString()} LinkBankVerifyAccount completed" + Environment.NewLine);
                                return response;
                            }
                            else
                            {
                                res.ReponseCode = 3;
                                res.status = false;
                                res.Message = "Failed";
                                res.Details = "Something Went Wrong Try Again Later!";
                                response = Request.CreateResponse<GetVerifyLinkBank>(System.Net.HttpStatusCode.BadRequest, res);
                                return response;
                            }
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
                log.Error($"{System.DateTime.Now.ToString()} RegisterVerification {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}