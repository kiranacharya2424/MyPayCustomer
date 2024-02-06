using log4net;
using MyPay.API.Models;
using MyPay.API.Models.Request;
using MyPay.API.Models.Response;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class RewardPointsController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(RewardPointsController));
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/rewardpoints")]
        public HttpResponseMessage RewardPointsListing(Req_RewardPoints user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside rewardpoints" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_RewardPoints result = new Res_RewardPoints();
            var response = Request.CreateResponse<Res_RewardPoints>(System.Net.HttpStatusCode.BadRequest, result);

            var userInput = getRawPostData().Result;

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
                    string md5hash = Common.getHashMD5(userInput); //Common.CheckHash(user);

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
                        string WalletBalance = string.Empty;
                        string TotalRewardPoints = string.Empty;
                        List<AddRewardPointTransactions> list = new List<AddRewardPointTransactions>();
                        string msg = RepUser.GetApiRewardPoints(resUser, user.DateFrom, user.DateTo, user.MemberId.ToString(), user.Take.ToString(), user.Skip.ToString(), ref list, ref WalletBalance, ref TotalRewardPoints);
                        if (msg.ToLower() == Common.CommonMessage.success)
                        {
                            result.WalletBalance = WalletBalance;
                            result.TotalRewardPoints = TotalRewardPoints;
                            result.RewardPoints = list;
                            result.Message = msg;
                            result.ReponseCode = 1;
                            result.status = true;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_RewardPoints>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            result.RewardPoints = list;
                            result.Message = msg;
                            result.ReponseCode = 0;
                            result.status = true;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_RewardPoints>(System.Net.HttpStatusCode.NotFound, result);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} rewardpoints completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} rewardpoints {e.ToString()} " + Environment.NewLine);
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} rewardpoints {ex.ToString()} " + Environment.NewLine);
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