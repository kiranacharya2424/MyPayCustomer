using log4net;
using MyPay.API.Models;
using MyPay.API.Models.State;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Response;
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
    public class GetCreditCardChargesController : ApiController
    {
        //private static ILog log = LogManager.GetLogger(typeof(GetCreditCardChargesController));
        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetCreditCardIsuuer(Req_CreditCardCharge user)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            log.Info($"{System.DateTime.Now.ToString()} inside GetCreditCardIsuuer" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_CreditCardCharge result = new Res_CreditCardCharge();
            var response = Request.CreateResponse<Res_CreditCardCharge>(System.Net.HttpStatusCode.BadRequest, result);

            var userInput = getRawPostData().Result;

            log.Info("credit card userInput : " + userInput);

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
                        AddUserLoginWithPin res = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        Int64 memId = 0;
                        int VendorAPIType = 0;
                        int Type = 0;
                        res = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                         //GetCreditCardCharge senddoc = RepPrabhu.GetCreditCardCharge(user.Amount.ToString(), user.Code);
                        GetCreditCardCharge senddoc = new GetCreditCardCharge();
                        ///AddCalculateServiceChargeAndCashback objOut = Common.CalculateNetAmountWithServiceCharge("0", user.Amount.ToString(), "80");

                        
                            senddoc.Code = user.Code;
                            senddoc.SCharge = "0".ToString();
                            senddoc.Message = "success";
                        

                        if (senddoc.Message.ToLower() == "success")
                        {
                            result.status = true;
                            result.ReponseCode = 1;
                            result.Message = "Success";
                            result.Details = "Success";
                            result.Charges = senddoc.SCharge;
                            response.StatusCode = HttpStatusCode.Created;
                        }
                        else
                        {
                            result.ReponseCode = 2;
                            result.Message = "Failed";
                            result.Details = senddoc.Message;
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} GetCreditCardIsuuer completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} GetCreditCardIsuuer {ex.ToString()} " + Environment.NewLine);
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