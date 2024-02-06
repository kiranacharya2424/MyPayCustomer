using log4net;
using MyPay.API.Models;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using System;
using System.Data.Entity.Validation;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class RequestAPI_TopupController : ApiController
    {
        string ApiResponse = string.Empty;

        private static ILog log = LogManager.GetLogger(typeof(RequestAPI_TopupController));
        [HttpPost]
        [Route("api/user-topup")]
        public HttpResponseMessage UserTopup_Post(Req_Vendor_API_Requests user)
        {

            VendorApi_CommonHelper.saveReceipt("123213", "Test servuce", "123456", "1234567890", "simple json content", "9803505220", "Roshan Thapa", "Topup", "9803505220", "3000");

            log.Info($" {System.DateTime.Now.ToString()} inside RequestAPI_TopupController  {Environment.NewLine}");
            string FinalResponse = string.Empty;
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_Requests result = new Res_Vendor_API_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_Requests>(System.Net.HttpStatusCode.BadRequest, result);

            string UserInput = getRawPostData().Result;
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
                    string md5hash = Common.getHashMD5(UserInput);  //Common.CheckHash(user);
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
                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_topup;
                            int Type = Convert.ToInt32(user.PaymentMode);
                            resUser = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, true, user.Amount, true, user.Mpin);
                            if (CommonResult.ToLower() != "success")
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                return response;
                            }
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = (!string.IsNullOrEmpty(user.PaymentMode) ? (user.PaymentMode) : "1");
                        user.Req_ReferenceNo = new CommonHelpers().GenerateUniqueId();
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string msg = RepKhalti.RequestVendorTopup(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.Req_ReferenceNo, user.Amount, user.Number, user.Remarks, user.MemberId,
                            authenticationToken, UserInput, user.DeviceCode, user.PlatForm, resUser, ref objVendor_API_Requests);
                        if (msg.ToLower() == "success" || msg.ToLower() == "pending")
                        {
                            result.Message = "Success";
                            result.Details = RepKhalti.resKhalti.Res_Khalti_Message;
                            result.ReponseCode = 1;
                            result.status = true;
                            result.WalletBalance = RepKhalti.WalletBalance;
                            result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                            result.TransactionUniqueId = TransactionID;
                            FinalResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                        }
                        AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                        GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                        inobjApiResponse.Id = objVendor_API_Requests.Id;
                        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                        if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id != 0)
                        {
                            resUpdateRecord.Res_Output = ApiResponse;
                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                        }
                    }
                }
                //RepKhalti.SaveAPIResponse(RepKhalti.Id, Newtonsoft.Json.JsonConvert.SerializeObject(FinalResponse));

                log.Info($"  {System.DateTime.Now.ToString()}   RequestAPI_TopupController Completed  {Environment.NewLine}");
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
                log.Error($"  {System.DateTime.Now.ToString()}   RequestAPI_TopupController {e.ToString()}  ");
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"  {System.DateTime.Now.ToString()}   RequestAPI_TopupController {ex.ToString()}  ");
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