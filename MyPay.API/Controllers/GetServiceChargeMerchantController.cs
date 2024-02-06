using log4net;
using MyPay.API.Models;
using MyPay.API.Models.Request;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Repository;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class GetServiceChargeMerchantController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(GetServiceChargeController));
        public HttpResponseMessage Post(Req_ServiceChargeMerchant user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside GetServiceChargeController" + Environment.NewLine);
            string WebPrefix = Common.LiveSiteUrl;
            Res_ServiceChargeMerchant_Requests result = new Res_ServiceChargeMerchant_Requests();
            var response = Request.CreateResponse<Res_ServiceChargeMerchant_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                    if (string.IsNullOrEmpty(user.MerchantId))
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage("Please provide MerchantId");
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    else
                    {
                        AddMerchant outobject = new AddMerchant();
                        GetMerchant inobject = new GetMerchant();
                        inobject.MerchantUniqueId = user.MerchantId;
                        AddMerchant model = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                        if (model != null && model.Id != 0)
                        {
                            AddCalculateServiceChargeAndCashback objRet = new AddCalculateServiceChargeAndCashback();
                            objRet = Common.CalculateNetAmountWithServiceChargeMerchant(user.MerchantId.ToString(), user.Amount.ToString(), user.ServiceId.ToString());
                            {
                                result.Message = "success";
                                result.ReponseCode = 1;
                                result.status = true;
                                result.MemberId = user.MemberId;
                                result.Amount = objRet.Amount.ToString("0.00");
                                result.DiscountAmount = objRet.DiscountAmount.ToString("0.00");
                                result.DiscountPercentage = objRet.DiscountPercentage.ToString("0.00");
                                result.CashbackAmount = objRet.CashbackAmount.ToString("0.00");
                                result.MerchantCommissionTotal = objRet.MerchantCommissionTotal.ToString("0.00");
                                result.ServiceChargeAmount = objRet.ServiceCharge.ToString("0.00");
                                result.PercentageServiceCharge = objRet.PercentageServiceCharge.ToString("0.00");
                                result.NetAmount = objRet.NetAmount.ToString("0.00");
                                result.RewardPoints = objRet.RewardPoints.ToString("0.00");
                                result.MPCoinsDebit = objRet.MPCoinsDebit.ToString("0.00");
                                result.WalletAmountDeduct = (objRet.Amount - objRet.MPCoinsDebit).ToString("0.00");
                                response.StatusCode = HttpStatusCode.Accepted;
                                response = Request.CreateResponse<Res_ServiceChargeMerchant_Requests>(System.Net.HttpStatusCode.OK, result);
                                log.Info($"{System.DateTime.Now.ToString()} GetServiceChargeController completed" + Environment.NewLine);
                                return response;
                            }
                        }
                        else
                        {

                            CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage("Invalid MerchantId");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} GetServiceChargeController {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}