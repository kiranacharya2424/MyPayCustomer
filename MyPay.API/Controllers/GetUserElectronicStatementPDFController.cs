using log4net;
using MyPay.API.Models;
using MyPay.API.Models.Request;
using MyPay.API.Models.Response;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
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
    public class GetUserElectronicStatementPDFController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(GetTransactionController));
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/getuserestatement")]
        public HttpResponseMessage GetEStatement(Req_EStatement user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside GetEStatement" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Estatement result = new Res_Estatement();
            var response = Request.CreateResponse<Res_Estatement>(System.Net.HttpStatusCode.BadRequest, result);

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
                        string msg = "";
                        Int64 Id = 0;
                        string token = DateTime.Now.Ticks.ToString();
                        AddEstatementPDFToken outobject = new AddEstatementPDFToken();
                        outobject.MemberId = user.MemberId;
                        outobject.Token = token;
                        outobject.Month = user.Month;
                        outobject.Year = user.Year;
                        outobject.FromDate = user.FromDate;
                        outobject.ToDate = user.ToDate;
                        string[] arrFromDate = user.FromDate.Split('-');
                        string[] arrToDate = user.ToDate.Split('-');

                        if (user.FromDate != "" && user.ToDate != "")
                        {
                            double TotalDays = (Convert.ToDateTime(user.ToDate) - Convert.ToDateTime(user.FromDate)).TotalDays;
                            if (TotalDays >= 0 && TotalDays <= 30)
                            {
                                Id = RepCRUD<AddEstatementPDFToken, GetEstatementPDFToken>.Insert(outobject, "estatementpdftoken");
                            }
                            else if (Convert.ToDateTime(user.ToDate) < Convert.ToDateTime(user.FromDate))
                            {
                                msg = "Invalid Date Range";
                            }
                            else
                            {
                                msg = "You can download pdf of 1 month maximum.";
                            }
                        }

                        if (Id > 0)
                        {
                            msg = "success";
                        }

                        if (msg.ToLower() == Common.CommonMessage.success)
                        {
                            string BaseURL = Common.LiveSiteUrl;
                            if (!Common.ApplicationEnvironment.IsProduction)
                            {
                                BaseURL = Common.TestSiteUrl;
                            }
                            result.URL = BaseURL + "/EstatementPDF?Token=" + user.MemberId + "-" + token;
                            result.Message = msg;
                            result.ReponseCode = 1;
                            result.status = true;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Estatement>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            result.Message = msg;
                            result.ReponseCode = 0;
                            result.status = false;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.NotFound, result);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} GetEStatement completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} GetEStatement {ex.ToString()} " + Environment.NewLine);
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