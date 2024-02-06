using log4net;
using MyPay.API.Models;
using MyPay.API.Models.FonePay;
using MyPay.API.Models.Request;
using MyPay.API.Models.Request.NepalPayQR;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Get.FonePay;
using MyPay.Models.Miscellaneous;
using MyPay.Models.NepalPayQR;
using MyPay.Models.VendorAPI.Get.BusSewaService;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using Newtonsoft.Json;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web.Http;

using static MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper;

namespace MyPay.API.Controllers
{
    public class RequestAPI_FonePayController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(RequestAPI_FonePayController));

        string ApiResponse = string.Empty;

        [HttpPost]
        [Route("api/lookup-QRParse")]
        public HttpResponseMessage Get_QRParse(Req_Vendor_API_FonePay_QRParse_Lookup_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-FonePay-QRParse" + Environment.NewLine);
            string UserInput = getRawPostData().Result;
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_FonePay_QRRequest_Lookup_Requests result = new Res_Vendor_API_FonePay_QRRequest_Lookup_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_FonePay_QRRequest_Lookup_Requests>(System.Net.HttpStatusCode.BadRequest, result);
            GetUserInfoController MyPayQR = new GetUserInfoController();
            Req_GetUserInfo Mypayobj = new Req_GetUserInfo();
            Mypayobj.DeviceCode = user.DeviceCode;
            Mypayobj.MemberId = user.MemberId;
            Mypayobj.DeviceId = user.DeviceId;
            Mypayobj.Version = user.Version;
            Mypayobj.PlatForm = user.PlatForm;
            Mypayobj.TimeStamp = user.TimeStamp;
            Mypayobj.Token = user.Token;
            Mypayobj.UniqueCustomerId = user.UniqueCustomerId;
            Mypayobj.PaymentMode = user.PaymentMode;
            Mypayobj.BankTransactionId = user.BankTransactionId;
            Mypayobj.SecretKey = user.SecretKey;
            Mypayobj.Mpin = user.Mpin;
            Mypayobj.VendorJsonLookup = user.VendorJsonLookup;
            Mypayobj.CouponCode = user.CouponCode;
            Mypayobj.Hash = user.Hash;
            try
            {
                CommonResponseData NCHLresult = new CommonResponseData();
                var NCHLresponse = Request.CreateResponse<CommonResponseData>(System.Net.HttpStatusCode.BadRequest, NCHLresult);
                if (Request.Headers.Authorization == null)
                {
                    string results = "Un-Authorized Request";
                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
                   //NCHLresult.StatusCode = HttpStatusCode.BadRequest;
                    NCHLresult.StatusCode = HttpStatusCode.BadRequest.ToString();
                    NCHLresult.Message= "Un-Authorized Request";
                    NCHLresult.status = false;
                    NCHLresult.ReponseCode = 0;
                    response = Request.CreateResponse<CommonResponseData>(System.Net.HttpStatusCode.BadRequest, NCHLresult);
                    return response;
                }
                else
                {
                    string md5hash = Common.CheckHash(UserInput);

                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results != "Success")
                    {
                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
                        //response.StatusCode = HttpStatusCode.BadRequest;
                        //response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        NCHLresult.StatusCode = HttpStatusCode.BadRequest.ToString();
                        NCHLresult.Message = cres.Message;
                        NCHLresult.status = false;
                        NCHLresult.ReponseCode = 0;
                        response = Request.CreateResponse<CommonResponseData>(System.Net.HttpStatusCode.BadRequest, NCHLresult);
                        return response;
                    }
                    else
                    {
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();

                        Int64 memId = 0;
                        int VendorAPIType = 0;
                        int Type = 0;
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true, false);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            //response.StatusCode = HttpStatusCode.BadRequest;
                            //response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            NCHLresult.StatusCode = HttpStatusCode.BadRequest.ToString();
                            NCHLresult.Message = cres1.Message;
                            NCHLresult.status = false;
                            NCHLresult.ReponseCode = 0;
                            response = Request.CreateResponse<CommonResponseData>(System.Net.HttpStatusCode.BadRequest, NCHLresult);
                            return response;
                        }
                        if (string.IsNullOrEmpty(user.qrRequestMessage))
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage("qrRequestMessage Not Found");
                            //response.StatusCode = HttpStatusCode.BadRequest;
                            //response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            NCHLresult.StatusCode = HttpStatusCode.BadRequest.ToString();
                        NCHLresult.Message = cres1.Message;
                        NCHLresult.status = false;
                        NCHLresult.ReponseCode = 0;
                        response = Request.CreateResponse<CommonResponseData>(System.Net.HttpStatusCode.BadRequest, NCHLresult);
                            return response;
                        }


                        //var QRTypeFrom=string.Empty;
                        //-- Start Find QR type (either from fonepay or NepalPayQR)--//
                        string substring = "NCHL";
                        var checkfornchl = user.qrRequestMessage.Contains(substring);

                        string substring2 = "fonepay.com";
                        var checkforfonepay = user.qrRequestMessage.Contains(substring2);

                        string substring3 = "/";
                        var checkforMypaymerchant = user.qrRequestMessage.Contains(substring3);

                       
                        if (checkfornchl == true && checkforfonepay == false)
                        {
                            var FirstId = user.qrRequestMessage.Substring(0, 2);
                            var FirstLength = user.qrRequestMessage.Substring(2, 2);
                            var FirstValue = user.qrRequestMessage.Substring(4, Convert.ToInt32(FirstLength));

                            var firstsubstring = FirstId + FirstLength + FirstValue;
                            var replaceFirstString = user.qrRequestMessage.Replace(firstsubstring, "");

                            var SecondId = replaceFirstString.Substring(0, 2);
                            var SecondLength = replaceFirstString.Substring(2, 2);
                            var SecondValue = replaceFirstString.Substring(4, Convert.ToInt32(SecondLength));

                            var NCHLId = SecondValue.Substring(0, 2);
                            var NCHLLength = SecondValue.Substring(2, 2);
                            var NCHLValue = SecondValue.Substring(4, Convert.ToInt32(NCHLLength));
                            var nchl = NCHLValue.Contains(substring);
                            if (nchl == true)
                            {
                                //--- start to validate NCHL QR  ---//

                                string Reference = new CommonHelpers().GenerateUniqueId();
                                string authenticationToken = Request.Headers.Authorization.Parameter;
                                Common.authenticationToken = authenticationToken;
                                string Res_output = response.ToString();
                                var Rep_State = string.Empty;
                                var Rep_status = "0";

                                NepalQRAuthResponse model = Authentication(user, UserInput, authenticationToken); //Get Authentication
                                string instructionId ="MP" + new CommonHelpers().GenerateUniqueId_NepalPay();
                                if (!string.IsNullOrEmpty(model.access_token))
                                {
                                    GetDataFromNepalQRPay validateissuertoNPI = RepKhalti.GetRequestIssuerToNPI(instructionId, user.qrRequestMessage, user.Version, user.DeviceCode, user.PlatForm, Convert.ToInt64(user.MemberId), "", authenticationToken, UserInput, Convert.ToInt32(KhaltiAPIName.NepalPay_QR_Payments), model.access_token);
                                    if (validateissuertoNPI.IsException == false)
                                    {
                                        var dataresponse = JsonConvert.DeserializeObject<ValidateResponse>(validateissuertoNPI.message);

                                        if (dataresponse.responseCode == "000")
                                        {
                                            if (Common.ApplicationEnvironment.IsProduction == false)
                                            {
                                                dataresponse.encKeySerial = "6dace9d30005000002a9";
                                                dataresponse.issuerId = string.IsNullOrEmpty(dataresponse.issuerId) ? "00010016" : dataresponse.issuerId;
                                                dataresponse.narration = string.IsNullOrEmpty(dataresponse.narration) ? "Hello" : dataresponse.narration;
                                                dataresponse.instrument = string.IsNullOrEmpty(dataresponse.instrument) ? "WAL" : dataresponse.instrument;
                                             }
                                            else
                                            {
                                                dataresponse.encKeySerial = "6dace9d30005000002a9";
                                                dataresponse.issuerId = string.IsNullOrEmpty(dataresponse.issuerId) ? "00010012" : dataresponse.issuerId;
                                                dataresponse.narration = string.IsNullOrEmpty(dataresponse.narration) ? "Hello" : dataresponse.narration;
                                                dataresponse.instrument = string.IsNullOrEmpty(dataresponse.instrument) ? "WAL" : dataresponse.instrument;

                                            }
                                            dataresponse.qrString = string.IsNullOrEmpty(dataresponse.qrString) ? user.qrRequestMessage : dataresponse.qrString;
                                            NCHLresult.status = true;
                                            NCHLresult.ReponseCode = 1;
                                            NCHLresult.Message = "success";
                                            response.StatusCode = HttpStatusCode.Accepted;
                                            NCHLresult.Data = dataresponse;
                                            NCHLresult.StatusCode = response.StatusCode.ToString();
                                            Res_output = JsonConvert.SerializeObject(NCHLresult);
                                            Rep_State = "Success";
                                            Rep_status = "1";
                                            NCHLresult.value1 = "NCHL";
                                            NCHLresult.value2 = "payment/issuer-to-NPI-payment";
                                            response = Request.CreateResponse<CommonResponseData>(System.Net.HttpStatusCode.OK, NCHLresult);
                                        }
                                        else
                                        {
                                            NCHLresult.Message = dataresponse.responseMessage;
                                            NCHLresult.status = dataresponse.responseCode == "000" ? true : false;
                                            NCHLresult.ReponseCode = dataresponse.responseCode == "000" ? 1 : 0;
                                            response.StatusCode = HttpStatusCode.BadRequest;
                                            NCHLresult.Data = validateissuertoNPI.message;
                                            NCHLresult.StatusCode = response.StatusCode.ToString();
                                            Res_output = JsonConvert.SerializeObject(NCHLresult);
                                            Rep_State = "Failed";
                                            NCHLresult.value1 = "NCHL";
                                            response = Request.CreateResponse<CommonResponseData>(System.Net.HttpStatusCode.BadRequest, NCHLresult);

                                        }

                                    }
                                    else
                                    {
                                        log.Error($"{System.DateTime.Now.ToString()}lookup-QRParse {validateissuertoNPI.message.ToString()} " + Environment.NewLine);
                                        NCHLresult = CommonApiMethod.AllReturnBadRequestMessage(validateissuertoNPI.message);
                                        response.StatusCode = HttpStatusCode.BadRequest;
                                        Res_output = JsonConvert.SerializeObject(NCHLresult);
                                        NCHLresult.value1 = "NCHL";
                                        response = Request.CreateResponse<CommonResponseData>(System.Net.HttpStatusCode.BadRequest, NCHLresult);
                                        Rep_State = "Failed";
                                    }
                                    if (!string.IsNullOrEmpty(validateissuertoNPI.Id))
                                    {
                                        var jsondata = Res_output;
                                        MyPay.Models.Common.CommonHelpers commonHelpers = new MyPay.Models.Common.CommonHelpers();
                                        string Result = commonHelpers.GetScalarValueWithValue("update Vendor_API_Requests set Res_Khalti_State='" + Rep_State + "',Res_khalti_Status=" + Rep_status + ", Res_Output='" + Res_output + "' where Id=" + validateissuertoNPI.Id + "");

                                    }
                                }
                                else
                                {
                                    NCHLresult = CommonApiMethod.AllReturnBadRequestMessage(CommonResult);
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    NCHLresult.value1 = "NCHL";
                                    response = Request.CreateResponse<CommonResponseData>(System.Net.HttpStatusCode.BadRequest, NCHLresult);
                                    return response;
                                }


                            }
                            else
                            {
                                NCHLresult.value1 = "Not found";
                            }

                        }
                        else if (checkfornchl == false && checkforfonepay == true)
                        {
                            //-- start of fonepay -- //


                            GetVendor_API_FonePay_QR_Parse_Lookup objReq = new GetVendor_API_FonePay_QR_Parse_Lookup();
                            GetVendor_API_FonePay_QR_Parse_Lookup_Response objRes = new GetVendor_API_FonePay_QR_Parse_Lookup_Response();
                            objReq.qrRequestMessage = user.qrRequestMessage;
                            Common.AddLogs($"FonePay QR: {Newtonsoft.Json.JsonConvert.SerializeObject(objReq)}", true, (int)AddLog.LogType.DBLogs);
                            string json = FonePayCommon.FormatQR(Newtonsoft.Json.JsonConvert.SerializeObject(objReq));
                            VendorApi_CommonHelper.FonePayAPIURL = "fonepayQrSwitch/qr/qrRequest";
                            //var response1 = Request.CreateResponse<Res_Vendor_API_FonePay_QRRequest_Lookup_Requests>(System.Net.HttpStatusCode.BadRequest, result);
                            // response1 = Request.CreateResponse<CommonResponseData>(System.Net.HttpStatusCode.BadRequest, NCHLresult);
                          
                            string Response = FonePayCommon.GetFonePayQRRequest(json, ref objRes);
                            //return response1;
                            if (Response.ToLower() == "success")
                            {
                                result.payloadFormatIndicator = objRes.payloadFormatIndicator;
                                result.pointOfInitiationMethod = objRes.pointOfInitiationMethod;
                                result.fonepayMerchantAccountInformation = objRes.fonepayMerchantAccountInformation;
                                result.merchantCategoryCode = objRes.merchantCategoryCode;
                                result.transactionCurrency = objRes.transactionCurrency;
                                result.transactionAmount = objRes.transactionAmount;
                                if (string.IsNullOrEmpty(objRes.transactionAmount) || objRes.transactionAmount.ToLower().Contains("null"))
                                {
                                    result.transactionAmount = "0";
                                }
                                result.countryCode = objRes.countryCode;
                                result.merchantName = objRes.merchantName;
                                result.merchantCity = objRes.merchantCity;
                                result.additionalDataFieldTemplate = objRes.additionalDataFieldTemplate;
                                result.serverResponse = objRes.serverResponse;
                                result.qrRequestMessage = user.qrRequestMessage;
                                result.fonepayDiscountInformation = objRes.fonepayDiscountInformation;
                                result.fonepayChargeInformation = objRes.fonepayChargeInformation;
                                result.cyclicRedundancyCheck = objRes.cyclicRedundancyCheck;

                                result.ReponseCode = objRes.serverResponse.success ? 1 : 0;
                                result.status = objRes.serverResponse.success;
                                result.Message = objRes.serverResponse.success ? "success" : "failed";
                                response.StatusCode = HttpStatusCode.Accepted;
                                NCHLresult.status = true;
                                NCHLresult.ReponseCode = 1;
                                NCHLresult.Message = "success";
                                response.StatusCode = HttpStatusCode.Accepted;
                                NCHLresult.Data = result;
                                NCHLresult.StatusCode = response.StatusCode.ToString();
                                NCHLresult.value1 = "FONEPAY";
                                NCHLresult.value2 = "use-FonePay-QRPayment";
                                response = Request.CreateResponse<CommonResponseData>(System.Net.HttpStatusCode.BadRequest, NCHLresult);
                                // response = Request.CreateResponse<Res_Vendor_API_FonePay_QRRequest_Lookup_Requests>(System.Net.HttpStatusCode.OK, result);
                            }
                            else
                            {
                                NCHLresult = CommonApiMethod.AllReturnBadRequestMessage(Response);
                                NCHLresult.status = false;
                                response.StatusCode = HttpStatusCode.BadRequest;
                                NCHLresult.StatusCode = response.StatusCode.ToString();
                                NCHLresult.value1 = "FONEPAY";
                                response = Request.CreateResponse<CommonResponseData>(System.Net.HttpStatusCode.BadRequest, NCHLresult);
                                return response;
                                //cres = CommonApiMethod.ReturnBadRequestMessage(Response);
                                //response.StatusCode = HttpStatusCode.BadRequest;
                                //response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            }
                        }

                        //else if (checkforMypaymerchant == true)
                        //{
                        //    // GetUserInfo
                        //    Mypayobj.MemberId = Convert.ToString(memId);
                        //    Mypayobj.RecieverMemberId = Convert.ToString(user.qrRequestMessage);
                        //    CommonResponseData tes = MyPayQR.GetUserQR(Mypayobj, resuserdetails);
                        //    response = Request.CreateResponse<CommonResponseData>(System.Net.HttpStatusCode.OK, tes);
                        //    return response;
                        //}
                        else
                        {

                            Mypayobj.MemberId = Convert.ToString(memId);
                            Mypayobj.RecieverMemberId = Convert.ToString(user.qrRequestMessage);
                            CommonResponseData  tes = MyPayQR.GetUserQR(Mypayobj,resuserdetails);
                            response = Request.CreateResponse<CommonResponseData>(System.Net.HttpStatusCode.OK, tes);
                            return response;
                        }




                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-QRParse completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-QRParse {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/lookup-FonePay-QRParse")]
        public HttpResponseMessage GetFonePay_QRParse(Req_Vendor_API_FonePay_QRParse_Lookup_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside lookup-FonePay-QRParse" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_FonePay_QRRequest_Lookup_Requests result = new Res_Vendor_API_FonePay_QRRequest_Lookup_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_FonePay_QRRequest_Lookup_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();

                        Int64 memId = 0;
                        int VendorAPIType = 0;
                        int Type = 0;
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true, false);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        if (string.IsNullOrEmpty(user.qrRequestMessage))
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage("qrRequestMessage Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        GetVendor_API_FonePay_QR_Parse_Lookup objReq = new GetVendor_API_FonePay_QR_Parse_Lookup();
                        GetVendor_API_FonePay_QR_Parse_Lookup_Response objRes = new GetVendor_API_FonePay_QR_Parse_Lookup_Response();
                        objReq.qrRequestMessage = user.qrRequestMessage;
                        Common.AddLogs($"FonePay QR: {Newtonsoft.Json.JsonConvert.SerializeObject(objReq)}", true, (int)AddLog.LogType.DBLogs);
                        string json = FonePayCommon.FormatQR(Newtonsoft.Json.JsonConvert.SerializeObject(objReq));
                        VendorApi_CommonHelper.FonePayAPIURL = "fonepayQrSwitch/qr/qrRequest";
                        string Response = FonePayCommon.GetFonePayQRRequest(json, ref objRes);
                        if (Response.ToLower() == "success")
                        {
                            result.payloadFormatIndicator = objRes.payloadFormatIndicator;
                            result.pointOfInitiationMethod = objRes.pointOfInitiationMethod;
                            result.fonepayMerchantAccountInformation = objRes.fonepayMerchantAccountInformation;
                            result.merchantCategoryCode = objRes.merchantCategoryCode;
                            result.transactionCurrency = objRes.transactionCurrency;
                            result.transactionAmount = objRes.transactionAmount;
                            if (string.IsNullOrEmpty(objRes.transactionAmount) || objRes.transactionAmount.ToLower().Contains("null"))
                            {
                                result.transactionAmount = "0";
                            }
                            result.countryCode = objRes.countryCode;
                            result.merchantName = objRes.merchantName;
                            result.merchantCity = objRes.merchantCity;
                            result.additionalDataFieldTemplate = objRes.additionalDataFieldTemplate;
                            result.serverResponse = objRes.serverResponse;
                            result.qrRequestMessage = user.qrRequestMessage;
                            result.fonepayDiscountInformation = objRes.fonepayDiscountInformation;
                            result.fonepayChargeInformation = objRes.fonepayChargeInformation;
                            result.cyclicRedundancyCheck = objRes.cyclicRedundancyCheck;

                            result.ReponseCode = objRes.serverResponse.success ? 1 : 0;
                            result.status = objRes.serverResponse.success;
                            result.Message = objRes.serverResponse.success ? "success" : "failed";
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_FonePay_QRRequest_Lookup_Requests>(System.Net.HttpStatusCode.OK, result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(Response);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} lookup-FonePay-QRParse completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} lookup-FonePay-QRParse {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public NepalQRAuthResponse Authentication(Req_Vendor_API_FonePay_QRParse_Lookup_Requests user, string UserInput, string authenticationToken)
        {
            //string UserInput = getRawPostData().Result;
            NepalQRAuthResponse model = new NepalQRAuthResponse();
            GetDataFromNepalQRPay getroutesdetail = RepKhalti.AuthenticationNepalPayQR(user.Version, user.DeviceCode, user.PlatForm, Convert.ToInt64(user.MemberId), "", authenticationToken, UserInput, Convert.ToInt32(KhaltiAPIName.NepalPay_QR_Payments));
            model = JsonConvert.DeserializeObject<NepalQRAuthResponse>(Convert.ToString(getroutesdetail.message));
            return model;
        }



        [HttpPost]
        [Route("api/use-FonePay-QRPayment")]
        public HttpResponseMessage GetFonePay_QRPayment(Req_Vendor_API_FonePay_QRPayment_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-FonePay-QRParse" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_FonePay_Payments_Requests result = new Res_Vendor_API_FonePay_Payments_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_FonePay_Payments_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId.Trim() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.FonePay_QR_Payments;
                            int Type = ((int)WalletTransactions.WalletTypes.FonePay);
                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, true, user.Amount, true, user.Mpin, "", true);
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

                        if (string.IsNullOrEmpty(user.Purpose))
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage("Please enter Purpose");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        if (string.IsNullOrEmpty(user.Remarks))
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage("Please enter Remarks");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        GetVendor_API_FonePay_Payment_Response objRes = new GetVendor_API_FonePay_Payment_Response();

                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        user.PaymentMode = ((int)WalletTransactions.WalletTypes.FonePay).ToString();
                        user.Reference = new CommonHelpers().GenerateUniqueId();


                        // GET QR REQUEST DATA //

                        GetVendor_API_FonePay_QR_Parse_Lookup objReq = new GetVendor_API_FonePay_QR_Parse_Lookup();
                        objReq.qrRequestMessage = user.qrRequestMessage;
                        string json = FonePayCommon.FormatQR(Newtonsoft.Json.JsonConvert.SerializeObject(objReq));
                        VendorApi_CommonHelper.FonePayAPIURL = "fonepayQrSwitch/qr/qrRequest";
                        GetVendor_API_FonePay_QR_Parse_Lookup_Response objResQR = new GetVendor_API_FonePay_QR_Parse_Lookup_Response();
                        string Response = FonePayCommon.GetFonePayQRRequest(json, ref objResQR);

                        if (Response.ToLower() == "success")
                        {

                            // PAYMENT REQUEST OBJECT //

                            FonePay_CardAcceptor objCA = new FonePay_CardAcceptor();
                            FonePay_Address objFPAddr = new FonePay_Address();
                            objFPAddr.city = objResQR.merchantCity;
                            objFPAddr.country = objResQR.countryCode;
                            objCA.name = objResQR.merchantName;
                            objCA.address = objFPAddr;

                            // SET MERCHANT NAME
                            user.UniqueCustomerId = objResQR.merchantName;

                            string MerchantPAN_Encrypted = RepFonePay.FonePay_EncryptData_From_Publickey(objResQR.fonepayMerchantAccountInformation.merchantPan);
                            //string MerchantPAN_Encrypted = RepFonePay.FonePay_EncryptData_From_Publickey(VendorApi_CommonHelper.FonePay_MerchantPAN);
                            string TestUserAccountnoBank_Encrypted = RepFonePay.FonePay_EncryptData_From_Publickey(VendorApi_CommonHelper.FonePay_UserAccountnoBank);
                            //string TestUserAccountnoBank_Encrypted = "K1fBtok31O/wGrR2gS9edwR+JtzRLy1DRB1bH1HmSy+4GxAb3txwyr7TbiKHWaTBPJX3D3t5HOHrhUAMG8zjOUmCOEwCpus+k96y+7RxCVcvGIiUz/odu15yfMAjDndeiMzI9SfH/HSg5xG+8JmHHH15XSX9ns3tNsWB4IjWf6Rsr4RYYtyUzs9LdXY69LO7I8HCAO12wDp/U774RLhcMuZCHFf5vKUY4PFEDaO1Dk88chIxSfuMrhOCZcQ5Af0lRFzZZaaGA15Uhh7GQo83RayfKeJQrZy4Wh1AxgztHfBxzphBETZH2gTV8heK3fq3nYYKL2V2evjtpTP4Z2yuqg==";
                            string SampleValue = $"{VendorApi_CommonHelper.FonePay_Bank_UserName}:{VendorApi_CommonHelper.FonePay_Bank_Password}";
                            string ibftUserNamePwdEncrypt = RepFonePay.FonePay_EncryptData_From_Publickey(VendorApi_CommonHelper.FonePay_Bank_Password_Encrypted, false, true);
                            //string ibftUserNamePwdEncrypt = "WVcsMcTnsVfbmBmWscm1rkkoEus3+l/RfzubX1PK2X/gxHIUTT0mROulpFd3g7B8bvyns+Nw/wxxCs2tR/NYK3AuCMsappWOFTl0SSrMSZyxKkYInvpVi2Mw7r9PYrv+uW5LNBFhAR/uTX6tBHwkiIsdgUimzbcH21xg6gxH5a2mUQfdf72MsTDQlDSEYWCBaccPuC9h8IzsGjMrN4YcoQ+BAUL9Bf74xOkFKIQ+j7HmiWYyr3yZdln9hSVuWCXJe71NdPPvLl2p5RwirCDK+pM5bsbUOOC1ntTgWZ39IusVJDASxw6Qa4r6omRZp57wEz7UofJmy+2/9W6WdL2GlA==";
                            GetVendor_API_FonePay_Payment_Request objPayReq = new GetVendor_API_FonePay_Payment_Request();
                            objPayReq.acquirerCountryCode = objResQR.countryCode;
                            objPayReq.acquiringBin = objResQR.fonepayMerchantAccountInformation.acquiringBin;
                            objPayReq.issuerBin = VendorApi_CommonHelper.FonePay_IssuerBin;
                            objPayReq.amount = user.Amount;
                            objPayReq.businessApplicationId = "MP";
                            objPayReq.cardAcceptor = objCA;
                            objPayReq.localTransactionDateTime = System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                            objPayReq.recipientPrimaryAccountNumber = MerchantPAN_Encrypted;
                            objPayReq.retrievalReferenceNumber = Common.RandomNumber(1111, 9999).ToString() + Common.RandomNumber(1111, 9999).ToString() + Common.RandomNumber(1111, 9999).ToString();
                            objPayReq.secondaryId = objResQR.fonepayMerchantAccountInformation.qrLogId.ToString();
                            objPayReq.senderAccountNumber = TestUserAccountnoBank_Encrypted;
                            objPayReq.senderName = VendorApi_CommonHelper.FonePay_SenderName;
                            //objPayReq.senderMobileNumber = VendorApi_CommonHelper.FonePay_SenderMobileNumber;
                            //objPayReq.senderName = resGetRecord.FirstName + " " + resGetRecord.LastName;
                            objPayReq.senderMobileNumber = resGetRecord.ContactNumber;

                            objPayReq.transactionCurrencyCode = objResQR.transactionCurrency;
                            objPayReq.merchantCategoryCode = objResQR.merchantCategoryCode;
                            objPayReq.remarks1 = user.Remarks;
                            objPayReq.ibftUserNamePwdEncrypt = ibftUserNamePwdEncrypt;
                            bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                            string msg = RepFonePay.RequestFonePay_QR_Payment(ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, resGetRecord, user.BankTransactionId, user.PaymentMode, user.UniqueCustomerId, user.Value, user.Amount, user.Reference, user.Version, user.DeviceCode, user.PlatForm, user.MemberId, user.Remarks, user.Purpose, user.ReceiverAccountNumber, user.SenderAccountNumber, user.SenderMobile, user.SenderName, objResQR.fonepayMerchantAccountInformation.merchantPan, authenticationToken, UserInput, objPayReq, objReq.qrRequestMessage, ref objRes, ref objVendor_API_Requests);
                            if (msg.ToLower() == "success")
                            {
                                result.ReponseCode = objRes.responseCode == "RES000" ? 1 : 0;
                                result.status = objRes.responseCode == "RES000";
                                result.UniqueTransactionId = objRes.transactionIdentifier;
                                result.TransactionUniqueId = TransactionID;
                                result.transactionIdentifier = objRes.transactionIdentifier;
                                result.actionCode = objRes.actionCode;
                                result.traceId = objRes.traceId;
                                result.transmissionDateTime = objRes.transmissionDateTime;
                                result.merchantCategoryCode = objRes.merchantCategoryCode;
                                result.merchantName = objResQR.merchantName;
                                result.retrievalReferenceNumber = objRes.retrievalReferenceNumber;
                                result.responseMessage = "Transaction completed successfully with Reference No. " + objRes.retrievalReferenceNumber;
                                result.Details = result.responseMessage;
                                result.Message = "Success";
                                result.WalletBalance = objRes.WalletBalance;
                                result.CouponCode = resGetCouponsScratched.CouponCode; result.IsCouponUnlocked = IsCouponUnlocked;
                                response.StatusCode = HttpStatusCode.Accepted;
                                response = Request.CreateResponse<Res_Vendor_API_FonePay_Payments_Requests>(System.Net.HttpStatusCode.OK, result);
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
                        else
                        {

                            cres = CommonApiMethod.ReturnBadRequestMessage("Invalid QR Code");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} use-FonePay-QRParse completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-FonePay-QRParse {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



        [HttpPost]
        [Route("api/use-FonePay-QRPayment-CheckStatus")]
        public HttpResponseMessage GetFonePay_QRPaymentCheckStatus(Req_Vendor_API_FonePay_QRPayment_Status_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside use-FonePay-QRParse" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_Vendor_API_FonePay_Payments_Requests result = new Res_Vendor_API_FonePay_Payments_Requests();
            var response = Request.CreateResponse<Res_Vendor_API_FonePay_Payments_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId.Trim() != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.FonePay_QR_Payments;
                            int Type = ((int)WalletTransactions.WalletTypes.FonePay);
                            AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true, true);
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
                        GetVendor_API_FonePay_Payment_Response objRes = new GetVendor_API_FonePay_Payment_Response();

                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;


                        // GET QR REQUEST DATA //

                        GetVendor_API_FonePay_QR_Parse_CheckStatus_Request objReq = new GetVendor_API_FonePay_QR_Parse_CheckStatus_Request();
                        objReq.issuerBin = user.issuerBin;
                        objReq.retrievalReferenceNumber = user.retrievalReferenceNumber;
                        objReq.qrRequestMessage = user.qrRequestMessage;
                        string json = FonePayCommon.FormatQR(Newtonsoft.Json.JsonConvert.SerializeObject(objReq));
                        VendorApi_CommonHelper.FonePayAPIURL = "fonepayQrSwitch/qr/checkStatus";
                        GetVendor_API_FonePay_QR_Parse_CheckStatus_Response objResQR = new GetVendor_API_FonePay_QR_Parse_CheckStatus_Response();
                        string Response = FonePayCommon.GetFonePayQRPayment_CheckStatus(json, ref objResQR);

                        if (Response.ToLower() == "success")
                        {

                            result.ReponseCode = objResQR.actionCode == "00" ? 1 : 0;
                            result.status = objResQR.actionCode == "00";
                            result.UniqueTransactionId = objResQR.transactionIdentifier;
                            result.transactionIdentifier = objResQR.transactionIdentifier;
                            result.retrievalReferenceNumber = objResQR.retrievalReferenceNumber;
                            result.traceId = objResQR.traceId;
                            result.actionCode = objResQR.actionCode;
                            result.merchantCategoryCode = objResQR.merchantCategoryCode;
                            result.transmissionDateTime = objResQR.transmissionDateTime;
                            result.Message = "Success";
                            result.responseMessage = "Transaction Completed Successfully with Reference No. " + objResQR.retrievalReferenceNumber;
                            result.Details = result.responseMessage;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_Vendor_API_FonePay_Payments_Requests>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(Response);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} use-FonePay-QRParse completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-FonePay-QRParse {ex.ToString()} " + Environment.NewLine);
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