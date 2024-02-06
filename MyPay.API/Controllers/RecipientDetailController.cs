using log4net;
using MyPay.API.Models;
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
    public class RecipientDetailController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(RecipientDetailController));
        public HttpResponseMessage Post(Req_RecipientDetail user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside RecipientDetailController" + Environment.NewLine);
            Res_RecipientDetail result = new Res_RecipientDetail();
            var response = Request.CreateResponse<Res_RecipientDetail>(System.Net.HttpStatusCode.BadRequest, result);

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
                    else
                    {
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

                        if (res != null && res.Id != 0)
                        {
                            AddUserLoginWithPin outobject_rec = new AddUserLoginWithPin();
                            GetUserLoginWithPin inobject_rec = new GetUserLoginWithPin();
                            inobject_rec.ContactNumber = user.RecipientPhone;
                            //AddUserLoginWithPin res_rec = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobject_rec, outobject_rec);
                            AddUserBasicInfo res_rec = new AddUserBasicInfo();
                            res_rec.ContactNumber = user.RecipientPhone;
                            res_rec.GetUserInformationBasic();
                            if (res_rec != null && res_rec.Id != 0)
                            {
                                if (res.MemberId == res_rec.MemberId)
                                {
                                    CommonResponse cres1 = new CommonResponse();
                                    cres1 = CommonApiMethod.ReturnBadRequestMessage("Receiver account is same as sender account. Please enter different contact no.");
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                                    return response;
                                }
                                else
                                {
                                    //if (!string.IsNullOrEmpty(res_rec.FirstName))
                                    //{
                                    //    result.Name = res_rec.FirstName.Substring(0, 1).ToUpper() + "*****" + " " + res_rec.MiddleName + " " + res_rec.LastName;
                                    //}
                                    //else
                                    //{
                                    //    result.Name = "*****" + " " + res_rec.MiddleName + " " + res_rec.LastName;
                                    //}
                                    result.Name = res_rec.FirstName + " " + res_rec.LastName;
                                    result.IsDetailUpdated = res_rec.FirstName != "" ? true : false;
                                    result.IsDetailUpdated = res_rec.FirstName != "" ? true : false;
                                    result.Message = "Success";
                                    result.ReponseCode = 1;
                                    result.status = true;
                                    response.StatusCode = HttpStatusCode.Accepted;
                                    response = Request.CreateResponse<Res_RecipientDetail>(System.Net.HttpStatusCode.OK, result);
                                    log.Info($"{System.DateTime.Now.ToString()} RecipientDetailController completed" + Environment.NewLine);
                                    return response;
                                }
                            }
                            else
                            {
                                CommonResponse cres1 = new CommonResponse();
                                cres1 = CommonApiMethod.ReturnBadRequestMessage("This number is not registered on MyPay");
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                                return response;
                            }
                        }
                        else
                        {
                            CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage("Application user not found ");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} RecipientDetailController {ex.ToString()} " + Environment.NewLine);
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