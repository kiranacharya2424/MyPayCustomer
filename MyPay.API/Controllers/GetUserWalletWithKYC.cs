using log4net;
using MyPay.API.Models;
using MyPay.API.Models.Request;
using MyPay.Models.Add;
using MyPay.Models.Common;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyPay.API.Controllers
{

    public class GetUserWalletWithKYCController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(GetUserWalletWithKYCController));

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/GetUserWalletWithKYC")]
        public HttpResponseMessage Post(Req_GetUserDetail user)
        {
            log.Info($" {System.DateTime.Now.ToString()} inside GetUserWalletWithKYCController  {Environment.NewLine}");
            string WebPrefix = Common.LiveSiteUrl;
            Res_UserWalletWithKYC result = new Res_UserWalletWithKYC();
            var response = Request.CreateResponse<Res_UserWalletWithKYC>(System.Net.HttpStatusCode.BadRequest, result);
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

                    if (string.IsNullOrEmpty(user.DeviceId))
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage("Please enter DeviceId");
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    else
                    {
                        string CommonResult = "";
                        AddUserLoginWithPin res = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
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
                        if (res != null && res.Id != 0)
                        {

                            string authenticationToken = Request.Headers.Authorization.Parameter;
                            string ApiUser = Common.GetCreatedByName(authenticationToken);

                            result.AndroidVersion = Common.AndroidVersion;
                            result.ios_version = Common.IosVersion;
                            result.IsKycVerified = res.IsKYCApproved;
                            result.MemberId = res.MemberId;
                            result.TotalAmount = res.TotalAmount.ToString("0.00");
                            result.RefCode = res.RefCode;
                            result.IsBankAdded = res.IsBankAdded > 0;

                            result.TotalRewardPoints = res.TotalRewardPoints.ToString("0.00");
                            result.TotalCashback = Convert.ToDecimal(GetCashbackSum(res.MemberId)).ToString("f2");
                            result.TotalTransactions = "0.00";///Convert.ToDecimal(Common.GetTotalTransactionsSum(res.MemberId)).ToString("f2");
                            result.IsActive = res.IsActive;
                            result.IsResetPasswordFromAdmin = res.IsResetPasswordFromAdmin;
                            result.IsDarkTheme = res.IsDarkTheme;
                            //result.IsDeviceActive = Convert.ToBoolean(GetDeviceActiveStatus(res.MemberId, user.DeviceCode));
                            //result.IsLogout = Convert.ToBoolean(GetDeviceLogoutStatus(res.MemberId, user.DeviceCode));
                            result.IsDeviceActive = true;
                            result.CreatedDateDt = res.CreatedDatedt;
                            result.MPCoinsFlushDateDt = GetMPCoinsFlushDate();
                            var json = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/apisettings.json"));
                            ApiSetting objApiSettings = new ApiSetting();
                            objApiSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiSetting>(json);
                            result.IsLogout = objApiSettings.IsActive;
                            result.Message = "Success";
                            result.ReponseCode = 1;
                            result.status = true;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_UserWalletWithKYC>(System.Net.HttpStatusCode.OK, result);
                            AddUserLoginWithPin objRes = new AddUserLoginWithPin();
                            if (string.IsNullOrEmpty(res.DeviceId))
                            {
                                res.DeviceId = user.DeviceId;
                                bool IsUpdated = objRes.UpdateDeviceId(res.Id, res.DeviceId);
                               // bool IsUpdated = objRes.UpdateDeviceId_New(res.Id, res.DeviceId, user.Version_Number);
                                
                            }
                            log.Info($" {System.DateTime.Now.ToString()} GetUserWalletWithKYCController completed  {Environment.NewLine}");
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
                log.Error($"  {System.DateTime.Now.ToString()}  GetUserWalletWithKYCController {ex.ToString()}  ");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }


        }


        private decimal GetCashbackSum(Int64 MemberId)
        {
            decimal data = 0;
            try
            {
                CommonHelpers obj = new CommonHelpers();
                string Result = "";
                string str = "select sum(amount) from WalletTransactions with(nolock) where MemberId='" + MemberId.ToString() + "'  and Type in (17,18,20) and Sign = 1 and IsActive=1 and IsDeleted=0";
                Result = obj.GetScalarValueWithValue(str);
                if (!string.IsNullOrEmpty(Result) && Result != "0")
                {
                    data = (decimal.Parse(Result));
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }
        private string GetMPCoinsFlushDate()
        {
            string data = "";
            try
            {
                CommonHelpers obj = new CommonHelpers();
                string Result = "";
                string str = "select top 1 FORMAT(MPCoinsDateSettings,'dd-MMM-yyyy') from ApiSettings";
                Result = obj.GetScalarValueWithValue(str);
                if (!string.IsNullOrEmpty(Result) && Result != "0")
                {
                    data = Result;
                }
            }
            catch (Exception ex)
            {

            }
            return data;
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