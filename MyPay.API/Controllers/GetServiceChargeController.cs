using log4net;
using MyPay.API.Models;
using MyPay.API.Models.Request;
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
    public class GetServiceChargeController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(GetServiceChargeController));
        public HttpResponseMessage Post(Req_ServiceCharge user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside GetServiceChargeController" + Environment.NewLine);
            string WebPrefix = Common.LiveSiteUrl;
            Res_ServiceCharge_Requests result = new Res_ServiceCharge_Requests();
            var response = Request.CreateResponse<Res_ServiceCharge_Requests>(System.Net.HttpStatusCode.BadRequest, result);

            string userInput = getRawPostData().Result;
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
                    AddUserLoginWithPin res = new AddUserLoginWithPin();
                    AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                    Int64 memId = user.MemberId;
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
                    AddCalculateServiceChargeAndCashback objRet = new AddCalculateServiceChargeAndCashback();
                    string msg = RepServiceCharge.GetServiceCharge(user.MemberId.ToString(), user.Amount.ToString(), user.ServiceId.ToString(), user.PlatForm, user.DeviceCode, true, ref objRet);
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
                        result.Amount = objRet.Amount.ToString("0.00");
                        result.CashbackAmount = objRet.CashbackAmount.ToString("0.00");
                        result.MemberId = user.MemberId;
                        result.ServiceChargeAmount = objRet.ServiceCharge.ToString("0.00");
                        result.PercentageServiceCharge = objRet.PercentageServiceCharge.ToString("0.00");
                        result.NetAmount = objRet.NetAmount.ToString("0.00");
                        result.RewardPoints = objRet.RewardPoints.ToString("0.00");
                        result.MPCoinsDebit = objRet.MPCoinsDebit.ToString("0.00");
                        result.WalletAmountDeduct = Convert.ToDecimal((objRet.Amount - objRet.MPCoinsDebit)).ToString("0.00");
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<Res_ServiceCharge_Requests>(System.Net.HttpStatusCode.OK, result);
                        log.Info($"{System.DateTime.Now.ToString()} GetServiceChargeController completed" + Environment.NewLine);
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} GetServiceChargeController {ex.ToString()} " + Environment.NewLine);
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