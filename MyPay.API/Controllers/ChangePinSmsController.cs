using log4net;
using MyPay.API.Models;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Repository;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class ChangePinSmsController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(ChangePinSmsController));
        public HttpResponseMessage Post(Req_ChangePinWithOTP user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside ChangePinSmsController" + Environment.NewLine);
            Res_ChangePin result = new Res_ChangePin();
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
                    string CommonResult = "";
                    AddUserLoginWithPin res = new AddUserLoginWithPin();
                    AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                    string md5hash = Common.CheckHash(user);
                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results.ToLower() != "success")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    else
                    {

                        //Int64 memId = 0;
                        //int VendorAPIType = 0;
                        //int Type = 0;
                        //res = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId,UserInput,user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true, true);
                        //if (CommonResult.ToLower() != "success")
                        //{
                        //    CommonResponse cres1 = new CommonResponse();
                        //    cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                        //    response.StatusCode = HttpStatusCode.BadRequest;
                        //    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                        //    return response;
                        //}

                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        string msg = RepUser.ChangePinWithOTP(ref res, user.Phonenumber, user.Password, user.Pin, user.ConfirmPin, user.OTP, true, user.PlatForm, user.DeviceCode);
                        if (msg.ToLower() != "success")
                        {
                            CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        else
                        {
                            result.Message = "MPIN Changed Successfully";
                            result.ReponseCode = 1;
                            result.status = true;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_ChangePin>(System.Net.HttpStatusCode.OK, result);
                            log.Info($"{System.DateTime.Now.ToString()} ChangePinSmsController completed" + Environment.NewLine);
                            return response;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} ChangePinSmsController {ex.ToString()}" + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }


        }
    }
}