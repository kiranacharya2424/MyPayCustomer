using log4net;
using MyPay.API.Models;
using MyPay.API.Models.Request.MyPayment;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class GetMyPaymentController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(GetMyPaymentController));

        string ApiResponse = string.Empty;

        [HttpPost]
        [Route("api/getmypayment")]
        public HttpResponseMessage GetMyPayments(Req_MyPayment user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside MyPayment" + Environment.NewLine);
            string UserInput = getRawPostData().Result;

            CommonResponse cres = new CommonResponse();
            List<GetMyPayments> list = new List<GetMyPayments>();

           Res_MyPayment result = new Res_MyPayment();
            var response = Request.CreateResponse<Res_MyPayment>(System.Net.HttpStatusCode.BadRequest, result);
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
                    //string md5hash = Common.CheckHash(user);
                    string md5hash = Common.getHashMD5(UserInput);

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
                        AddUserLoginWithPin res = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        Int64 memId = 0;
                        int VendorAPIType = 0;
                        int Type = 0;
                        res = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true, true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }

                        GetMyPayments obj = new GetMyPayments();
                        obj.ProviderTypeId = user.ProviderTypeId;
                        obj.memberID = user.memberID;
                        DataTable result1 = obj.Get(obj.ProviderTypeId, obj.memberID);
                        if (result1.Rows.Count > 0)
                        {
                            List<GetMyPayments> dataList = new List<GetMyPayments>();

                            foreach (DataRow row in result1.Rows)
                            {
                                GetMyPayments obj1 = new GetMyPayments
                                {
                                    //Id = int.Parse(row["ID"].ToString()),
                                    JsonData = row["JsonData"].ToString(),
                                    ProviderName = row["ProviderName"].ToString(),
                                    ProviderTypeId = row["ProviderTypeId"].ToString(),
                                    memberID = row["memberID"].ToString()
                                };

                                dataList.Add(obj1); // Add the object to the list
                            }

                            cres.Message = "Success";
                            cres.Data = dataList;
                            cres.status = true;
                            cres.ReponseCode = 1;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.OK, cres);
                            return response;
                        }

                        else
                        {
                            cres.Message = "Error";
                            cres.responseMessage ="error" ;
                            cres.status = false;
                            cres.ReponseCode = 0;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }

                        return response;

                    }

                }

                log.Info($"{System.DateTime.Now.ToString()} use-mypay-MyPayment completed" + Environment.NewLine);
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
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} use-GetMyPayment {ex.ToString()} " + Environment.NewLine);
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