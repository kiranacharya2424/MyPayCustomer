using log4net;
using MyPay.API.Models;
using MyPay.API.Models.Request;
using MyPay.API.Models.Response;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Repository;
using Newtonsoft.Json;
using System;
using System.Data.Entity.Validation;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class InternetBankingController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(InternetBankingController));
        [System.Web.Http.HttpPost]
        public HttpResponseMessage InternetBanking(Req_InternetBanking user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside InternetBanking" + Environment.NewLine);
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
                        if (user.MemberId == null || user.MemberId == 0)
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Please Enter Member Id");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        else if (string.IsNullOrEmpty(user.Remarks))
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Please Select Remarks");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        else if (user.Amount == null || user.Amount == 0)
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Please Enter Amount");
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
                        if (string.IsNullOrEmpty(resuserdetails.FirstName))
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Please Update Your Name In Profile First");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        if (string.IsNullOrEmpty(resuserdetails.Email))
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Please Update Your Email In Profile First");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        if (string.IsNullOrEmpty(resuserdetails.ContactNumber))
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Please Update Your Contact Number In Profile First");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        AddNPS nps = new AddNPS();
                        nps.MerchantId = RepNps.MerchantId;
                        nps.MerchantName = RepNps.MerchantName;
                        nps.MerchantTxnId = new CommonHelpers().GenerateUniqueId();
                        nps.PaymentType = "f";
                        nps.TransactionAmount = Convert.ToDecimal(user.Amount).ToString("f2");
                        nps.ChargeCategory = "c";
                        nps.TransactionRemarks = user.Remarks;
                        nps.FurtherCreditEnabled = "n";
                        nps.ValidityTime = DateTime.UtcNow.AddMinutes(10).ToString("yyyy-MM-ddTHH:mm:ss");
                        nps.CustomerName = resuserdetails.FirstName + " " + resuserdetails.LastName;
                        nps.CustomerMobile = resuserdetails.ContactNumber;
                        nps.CustomerEmail = resuserdetails.Email;

                        log.Info($"{System.DateTime.Now.ToString()} InternetBanking RepNps.HMACSHA512 started" + Environment.NewLine);
                        nps.Signature = RepNps.HMACSHA512((nps.ChargeCategory + nps.CustomerEmail + nps.CustomerMobile + nps.CustomerName + nps.FurtherCreditAccName + nps.FurtherCreditAccNumber + nps.FurtherCreditBank + nps.FurtherCreditEnabled + nps.MerchantId + nps.MerchantName + nps.MerchantTxnId + nps.PaymentType + nps.TransactionAmount + nps.TransactionRemarks + nps.ValidityTime));
                        log.Info($"{System.DateTime.Now.ToString()} InternetBanking RepNps.HMACSHA512 completed" + Environment.NewLine);

                        log.Info($"{System.DateTime.Now.ToString()} InternetBanking RepNps.PostMethod started" + Environment.NewLine);
                        string data = RepNps.PostMethod("V2/GeneratePaymentLink", JsonConvert.SerializeObject(nps));

                        log.Info($"{System.DateTime.Now.ToString()} InternetBanking RepNps.PostMethod completed" + Environment.NewLine);
                        if (!string.IsNullOrEmpty(data))
                        {
                            Res_InternetBanking res = JsonConvert.DeserializeObject<Res_InternetBanking>(data);
                            if (res.code == "0" && res.message == "Success")
                            {
                                res.ReponseCode = 1;
                                res.Message = "Success";
                                res.Details = "Link Generated Successfully";
                                res.status = true;
                                response.StatusCode = HttpStatusCode.Created;
                                response = Request.CreateResponse<Res_InternetBanking>(System.Net.HttpStatusCode.Created, res);
                                log.Info($"{System.DateTime.Now.ToString()} InternetBanking completed" + Environment.NewLine);
                                return response;
                            }
                            else
                            {
                                if (res.errors.Count > 0)
                                {
                                    res.Details = res.errors[0].error_message;
                                }
                                else
                                {
                                    res.Details = res.message;
                                }
                                res.Message = "Failed";
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<Res_InternetBanking>(System.Net.HttpStatusCode.BadRequest, res);
                                return response;
                            }

                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Data Not Foud");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
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
                log.Error($"{System.DateTime.Now.ToString()} InternetBanking {e.ToString()} " + Environment.NewLine);
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} InternetBanking {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}