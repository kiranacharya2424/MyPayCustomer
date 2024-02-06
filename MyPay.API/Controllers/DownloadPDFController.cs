using log4net;
using MyPay.API.Models;
using MyPay.API.Models.Response;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using MyPay.Models.VendorAPI.Get.BusSewaService;
using static MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper;
using System.Web.Caching;
using System.Data;
using System.Web.Script.Serialization;
using System.Collections;
using MyPay.API.Models.Airlines;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace MyPay.API.Controllers
{
    public class DownloadPDFController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(BusSewa_RequestAPIController));

        string ApiResponse = string.Empty;
        // GET: DownloadPDF
        [HttpPost]
        [Route("api/DownloadPDF")]


        public HttpResponseMessage DownloadPDF(Request_BusTicket_Download user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside download-ticket" + Environment.NewLine);
            string UserInput = getRawPostData().Result;
            commonresponsedata result = new commonresponsedata();

            var response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
            try
            {
                if (Request.Headers.Authorization == null)
                {
                    string results = "Un-Authorized Request";
                    result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                    return response;
                }
                else
                {
                    string md5hash = Common.getHashMD5(UserInput);
                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results != "Success")
                    {
                        result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                        return response;
                    }
                    else
                    {
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

                        Int64 memId = 0;
                        int VendorAPIType = 0;
                        int Type = 0;
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false);
                        if (CommonResult.ToLower() != "success")
                        {
                            result = CommonApiMethod.BusSewaReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                            return response;
                        }

                        Req_Vendor_API_Bussewa_Lookup_RequestsTrip objRes = new Req_Vendor_API_Bussewa_Lookup_RequestsTrip();
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        //var jsondata = JsonConvert.SerializeObject(user.seat);
                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        string Res_output = response.ToString();
                        var Rep_State = string.Empty;
                        var Rep_status = "0";
                        DownloadPDF downloaddetails = Get_TICKET_Download(user.MemberId, user.Reference, user.LogID, user.Version, user.DeviceCode, user.PlatForm, user.IsEmailSend, resuserdetails.Email, user.VendorApiType);
                        if (downloaddetails.IsException == false)
                        {
                            // GetVendor_API_BusSewa_Routes_Lookup obj = JsonConvert.DeserializeObject<GetVendor_API_BusSewa_Routes_Lookup>(getroutesdetail.message.ToString());

                            if (downloaddetails.status == true)
                            {
                                result.status = true;
                                result.ReponseCode = result.status ? 1 : 0;
                                result.Message = "success";
                                response.StatusCode = HttpStatusCode.Accepted;
                                result.Data = downloaddetails;
                                result.StatusCode = response.StatusCode.ToString();
                                response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.OK, result);
                            }
                            else
                            {
                                result = CommonApiMethod.BusSewaReturnBadRequestMessage(downloaddetails.Message);
                                result.status = false;
                                response.StatusCode = HttpStatusCode.BadRequest;
                                result.ReponseCode = 0;
                                result.Message = downloaddetails.Message;
                                Res_output = JsonConvert.SerializeObject(result);
                                response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                                Rep_State = "Failed";
                            }


                        }
                        else
                        {
                            log.Error($"{System.DateTime.Now.ToString()} download-ticket {downloaddetails.Message.ToString()} " + Environment.NewLine);
                            result = CommonApiMethod.BusSewaReturnBadRequestMessage(downloaddetails.Message);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            Res_output = JsonConvert.SerializeObject(result);
                            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
                            Rep_State = "Failed";
                        }

                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} download-ticket completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} download-ticket {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        public static DownloadPDF Get_TICKET_Download(string MemberId, string Reference, string LogID, string Version, string DeviceCode, string PlatForm, bool IsEmailSend, string Email, string vendortype)
        {
            DownloadPDF objRes = new DownloadPDF();
            if (string.IsNullOrEmpty(Reference) || Reference == "0")
            {
                objRes.Message = "Please enter Reference.";
            }
            else if (string.IsNullOrEmpty(LogID))
            {
                objRes.Message = "Please enter LogID.";
            }
            else if (string.IsNullOrEmpty(Email) && IsEmailSend == true)
            {
                objRes.Message = "Please enter ContactEmail.";
            }
            else if (string.IsNullOrEmpty(Version))
            {
                objRes.Message = "Please enter Version.";
            }
            else if (string.IsNullOrEmpty(DeviceCode))
            {
                objRes.Message = "Please enter DeviceCode.";
            }
            else if (string.IsNullOrEmpty(PlatForm))
            {
                objRes.Message = "Please enter PlatForm.";
            }

            if (string.IsNullOrEmpty(objRes.Message) && (string.IsNullOrEmpty(MemberId) || MemberId == "0"))
            {
                objRes.Message = "Please enter MemberId.";
            }
            else if (string.IsNullOrEmpty(objRes.Message) && (!string.IsNullOrEmpty(MemberId)))
            {
                Int64 Num;
                bool isNum = Int64.TryParse(MemberId, out Num);
                if (!isNum)
                {
                    objRes.Message = "Please enter valid MemberId.";
                }
            }
            if (string.IsNullOrEmpty(objRes.Message))
            {
                string fullpath = string.Empty;
                string KhaltiAPIURL = "download";
                string templatepath = string.Empty;
                string download = string.Empty;

                try
                {
                    if (vendortype == "200")  //--bus sewa ticket --//
                    {
                        objRes = VendorApi_CommonHelper.RequestBus_DOWNLOAD_TICKET(Reference, MemberId, LogID, "/Content/BusSewaTicketPDF", ref fullpath, VendorApi_CommonHelper.BusSewa_URL_Prefix_localhost + KhaltiAPIURL);
                        templatepath = "/Templates/bus-pdf.html";
                        download = " - Download your bus ticket ";
                    }
                    else if (vendortype == "300") //--cable car ticket --//
                    {

                    }

                    if (objRes.status && IsEmailSend)
                    {
                        #region SendEmailConfirmation
                        string mystring = File.ReadAllText(HttpContext.Current.Server.MapPath(templatepath));
                        string body = mystring;
                        string Subject = MyPay.Models.Common.Common.WebsiteName + download;
                        if (!string.IsNullOrEmpty(Email))
                        {
                            MyPay.Models.Common.Common.SendAsyncMail(Email, Subject, body, fullpath);
                        }
                        #endregion                        
                    }
                    else
                    {
                        objRes.Message = objRes.Message;
                    }
                }
                catch (WebException e)
                {
                    objRes.IsException = true;
                    if (e.Response == null && e.Message != null)
                    {
                        //Common.AddLogs("Error BusSewaPost: ApiName " + VendorApi_CommonHelper.BusSewa_URL_Prefix_localhost + ". Request: " + Data + " Response: " + e.Message, false, (int)AddLog.LogType.DBLogs);
                        objRes.Message = e.Message;

                    }
                    else
                    {
                        using (WebResponse response = e.Response)
                        {
                            HttpWebResponse httpResponse = (HttpWebResponse)response;
                            Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                            using (Stream Edata = response.GetResponseStream())
                            using (var reader = new StreamReader(Edata))
                            {
                                objRes.Message = reader.ReadToEnd();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    objRes.IsException = true;
                    objRes.Message = ex.Message;
                }
            }
            return objRes;
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