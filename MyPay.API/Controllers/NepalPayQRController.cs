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
using MyPay.Models;
using commonresponsedata = MyPay.Models.VendorAPI.Get.BusSewaService.commonresponsedata;
using System.Web.Caching;
using System.Data;
using System.Web.Script.Serialization;
using System.Collections;
using MyPay.API.Models.Request.NepalPayQR;
using MyPay.Models.NepalPayQR;
using System.Security.Cryptography;
using System.Text;
using System.Security.Cryptography.Xml;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml.css;
using System.Web.Http.Controllers;
using System.Threading;
using System.Web.Http.Results;
using System.Web.Http.ModelBinding;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.PeerToPeer;
using MyPay.API.Models.Antivirus.Bussewa;
using CSharp_easy_RSA_PEM;
using System.Globalization;
using System.Windows.Input;
using iText.Kernel.Pdf;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using Chilkat;
using StringBuilder = System.Text.StringBuilder;
using Org.BouncyCastle.Crypto;
using System.Runtime.ConstrainedExecution;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.OpenSsl;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Asn1.X9;
using Hashtable = System.Collections.Hashtable;
using Microsoft.IdentityModel.Tokens;
using BitMiracle.LibTiff.Classic;
using Swashbuckle.Swagger;
using Org.BouncyCastle.Asn1.Ocsp;
using MyPay;
using System.Security.Principal;
using System.Web.UI.WebControls;
using DeviceId;

namespace MyPay.API.Controllers
{
    public class NepalPayQRController : ApiController
    {
        //GET: NepalPayQR
        private static ILog log = LogManager.GetLogger(typeof(NepalPayQRController));

        //[Route("api/validate/issuer-to-NPI-validate")]
        //public HttpResponseMessage GetLookupService_NepalPayQR_Validate(Req_Vendor_API_NepalPayQR_Requests user)
        //{
        //    log.Info($"{System.DateTime.Now.ToString()} inside validate/issuer-to-NPI-validate" + Environment.NewLine);
        //    string UserInput = getRawPostData().Result;
        //    //CommonResponse cres = new CommonResponse();
        //    commonresponsedata result = new commonresponsedata();
        //    var response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
        //    try
        //    {
        //        if (Request.Headers.Authorization == null)
        //        {
        //            string results = "Un-Authorized Request";
        //            result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
        //            response.StatusCode = HttpStatusCode.BadRequest;
        //            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
        //            return response;
        //        }
        //        else
        //        {
        //            string md5hash = Common.getHashMD5(UserInput); //Common.CheckHash(user);
        //            string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
        //            if (results != "Success")
        //            {
        //                result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
        //                response.StatusCode = HttpStatusCode.BadRequest;
        //                response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
        //                return response;
        //            }
        //            else
        //            {

        //                string CommonResult = "";
        //                AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
        //                AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

        //                Int64 memId = 0;
        //                int VendorAPIType = 0;
        //                int Type = 0;
        //                resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false);
        //                if (CommonResult.ToLower() != "success")
        //                {
        //                    result = CommonApiMethod.BusSewaReturnBadRequestMessage(CommonResult);
        //                    response.StatusCode = HttpStatusCode.BadRequest;
        //                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
        //                    return response;
        //                }
        //                string Reference = new CommonHelpers().GenerateUniqueId();
        //                string authenticationToken = Request.Headers.Authorization.Parameter;
        //                Common.authenticationToken = authenticationToken;
        //                string Res_output = response.ToString();
        //                var Rep_State = string.Empty;
        //                var Rep_status = "0";
        //                NepalQRAuthResponse model = Authentication(user, UserInput, authenticationToken); //Get Authentication
        //                string instructionId = "MyPay" + new CommonHelpers().GenerateUniqueId_NepalPay();
        //                if (!string.IsNullOrEmpty(model.access_token))
        //                {
        //                    GetDataFromNepalQRPay validateissuertoNPI = RepKhalti.GetRequestIssuerToNPI(instructionId, user.qrString, user.Version, user.DeviceCode, user.PlatForm, Convert.ToInt64(user.MemberId), "", authenticationToken, UserInput, Convert.ToInt32(KhaltiAPIName.bus_sewa), model.access_token);
        //                    if (validateissuertoNPI.IsException == false)
        //                    {
        //                        var dataresponse = JsonConvert.DeserializeObject<ValidateResponse>(validateissuertoNPI.message);
        //                        if (dataresponse.responseCode == "000")
        //                        {
        //                            result.status = true;
        //                            result.ReponseCode = 1;
        //                            result.Message = "success";
        //                            response.StatusCode = HttpStatusCode.Accepted;
        //                            result.Data = dataresponse;
        //                            result.StatusCode = response.StatusCode.ToString();
        //                            Res_output = JsonConvert.SerializeObject(result);
        //                            Rep_State = "Success";
        //                            Rep_status = "1";
        //                            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.OK, result);
        //                        }
        //                        else
        //                        {
        //                            result.Message = dataresponse.responseMessage;
        //                            result.status = dataresponse.responseCode == "000" ? true : false;
        //                            result.ReponseCode = dataresponse.responseCode == "000" ? 1 : 0;
        //                            response.StatusCode = HttpStatusCode.BadRequest;
        //                            result.Data = validateissuertoNPI.message;
        //                            result.StatusCode = response.StatusCode.ToString();
        //                            Res_output = JsonConvert.SerializeObject(result);
        //                            Rep_State = "Failed";
        //                            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);

        //                        }

        //                    }
        //                    else
        //                    {
        //                        log.Error($"{System.DateTime.Now.ToString()} validate/issuer-to-NPI-validate {validateissuertoNPI.message.ToString()} " + Environment.NewLine);
        //                        result = CommonApiMethod.BusSewaReturnBadRequestMessage(validateissuertoNPI.message);
        //                        response.StatusCode = HttpStatusCode.BadRequest;
        //                        Res_output = JsonConvert.SerializeObject(result);
        //                        response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
        //                        Rep_State = "Failed";
        //                    }
        //                    if (!string.IsNullOrEmpty(validateissuertoNPI.Id))
        //                    {
        //                        var jsondata = Res_output;
        //                        MyPay.Models.Common.CommonHelpers commonHelpers = new MyPay.Models.Common.CommonHelpers();
        //                        string Result = commonHelpers.GetScalarValueWithValue("update Vendor_API_Requests set Res_Khalti_State='" + Rep_State + "',Res_khalti_Status=" + Rep_status + ", Res_Output='" + jsondata + "' where Id=" + validateissuertoNPI.Id + "");

        //                    }
        //                }
        //                else
        //                {
        //                    result = CommonApiMethod.BusSewaReturnBadRequestMessage(CommonResult);
        //                    response.StatusCode = HttpStatusCode.BadRequest;
        //                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
        //                    return response;
        //                }


        //            }

        //        }
        //        log.Info($"{System.DateTime.Now.ToString()} validate/issuer-to-NPI-validate completed" + Environment.NewLine);
        //        return response;
        //    }
        //    catch (DbEntityValidationException e)
        //    {
        //        foreach (var eve in e.EntityValidationErrors)
        //        {
        //            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
        //                eve.Entry.Entity.GetType().Name, eve.Entry.State);
        //            foreach (var ve in eve.ValidationErrors)
        //            {
        //                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
        //                    ve.PropertyName, ve.ErrorMessage);
        //            }
        //        }
        //        //throw new CustomResponseException(false, "Bad Request", 0, "Exception Error", "", "");
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error($"{System.DateTime.Now.ToString()} validate/issuer-to-NPI {ex.ToString()} " + Environment.NewLine);
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}

        //public NepalQRAuthResponse Authentication(Req_Vendor_API_NepalPayQR_Requests user, string UserInput, string authenticationToken)
        //{
        //    //string UserInput = getRawPostData().Result;
        //    NepalQRAuthResponse model = new NepalQRAuthResponse();
        //    GetDataFromNepalQRPay getroutesdetail = RepKhalti.AuthenticationNepalPayQR(user.Version, user.DeviceCode, user.PlatForm, Convert.ToInt64(user.MemberId), "", authenticationToken, UserInput, Convert.ToInt32(KhaltiAPIName.NepalPay_QR_Payments));
        //    model = JsonConvert.DeserializeObject<NepalQRAuthResponse>(Convert.ToString(getroutesdetail.message));
        //    return model;
        //}

        [HttpPost]
        [Route("api/payment/issuer-to-NPI-payment")]
        public HttpResponseMessage GetLookupService_NepalPayQR_Payment(NPITOpaymentRequest user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside payment/issuer-to-NPI-payment" + Environment.NewLine);

            string UserInput = getRawPostData().Result;
            CommonResponse cres = new CommonResponse();
            //CommonResponse cres = new CommonResponse();
            CommonResponseData result = new CommonResponseData();
            var response = Request.CreateResponse<CommonResponseData>(System.Net.HttpStatusCode.BadRequest, result);
            var Rep_State = string.Empty;
            var Rep_status = "0";
            ValidateResponse dataresponse = new ValidateResponse();
            string Res_output = response.ToString();
            try
            {
                if (Request.Headers.Authorization == null)
                {
                    result.StatusCode = HttpStatusCode.BadRequest.ToString();
                    result.Message = "Un-Authorized Request";
                    result.status = false;
                    result.ReponseCode = 0;
                    response = Request.CreateResponse<CommonResponseData>(System.Net.HttpStatusCode.BadRequest, result);
                    return response;
                }
                else
                {
                    string md5hash = Common.getHashMD5(UserInput); //Common.CheckHash(user);
                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results != "Success")
                    {
                        result.StatusCode = HttpStatusCode.BadRequest.ToString();
                        result.Message = cres.Message;
                        result.status = false;
                        result.ReponseCode = 0;
                        response = Request.CreateResponse<CommonResponseData>(System.Net.HttpStatusCode.BadRequest, result);
                        return response;
                    }
                    else
                    {

                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

                        Int64 memId = Convert.ToInt64(user.MemberId);
                        int VendorAPIType = 0;
                        int Type = 0;
                        resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, true, Convert.ToString(user.amount), true, user.Mpin, "", false);
                        //resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, true, "0", true, user.Mpin, "", false);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);                          
                            result.StatusCode = HttpStatusCode.BadRequest.ToString();
                            result.Message = cres1.Message;
                            result.status = false;
                            result.ReponseCode = 0;
                            response = Request.CreateResponse<CommonResponseData>(System.Net.HttpStatusCode.BadRequest, result);
                            return response;
                        }


                        #region Start validate QR API
                        //----------------------------------------------------------------- Start to validate qr -----------------------------------------------------------//

                        string Reference = new CommonHelpers().GenerateUniqueId();
                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        GetDataFromNepalQRPay getroutesdetail = RepKhalti.AuthenticationNepalPayQR(user.Version, user.DeviceCode, user.PlatForm, Convert.ToInt64(user.MemberId), "", authenticationToken, UserInput, Convert.ToInt32(KhaltiAPIName.NepalPay_QR_Payments));
                        NepalQRAuthResponse model = JsonConvert.DeserializeObject<NepalQRAuthResponse>(Convert.ToString(getroutesdetail.message));                       
                        string instructionId = "MP" + new CommonHelpers().GenerateUniqueId_NepalPay();
                        if (!string.IsNullOrEmpty(model.access_token))
                        {
                            GetDataFromNepalQRPay validateissuertoNPI = RepKhalti.GetRequestIssuerToNPI(instructionId, user.qrString, user.Version, user.DeviceCode, user.PlatForm, Convert.ToInt64(user.MemberId), "", authenticationToken, UserInput, Convert.ToInt32(KhaltiAPIName.NepalPay_QR_Payments), model.access_token);
                            if (validateissuertoNPI.IsException == false)
                            {
                                dataresponse = JsonConvert.DeserializeObject<ValidateResponse>(validateissuertoNPI.message);

                                if (dataresponse.responseCode == "000")
                                {
                                    
                                    dataresponse.qrString = string.IsNullOrEmpty(dataresponse.qrString) ? user.qrString : dataresponse.qrString;
                                    result.status = true;
                                    result.ReponseCode = 1;
                                    result.Message = "success";
                                    response.StatusCode = HttpStatusCode.Accepted;
                                    result.Data = dataresponse;
                                    result.StatusCode = response.StatusCode.ToString();
                                    Res_output = JsonConvert.SerializeObject(result);
                                    Rep_State = "Success";
                                    Rep_status = "1";
                                    result.value1 = "NCHL";
                                    result.value2 = "payment/issuer-to-NPI-payment";
                                    response = Request.CreateResponse<CommonResponseData>(System.Net.HttpStatusCode.OK, result);
                                }
                                else
                                {
                                    result.Message = dataresponse.responseMessage;
                                    result.status = dataresponse.responseCode == "000" ? true : false;
                                    result.ReponseCode = dataresponse.responseCode == "000" ? 1 : 0;
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    result.Data = validateissuertoNPI.message;
                                    result.StatusCode = response.StatusCode.ToString();
                                    Res_output = JsonConvert.SerializeObject(result);
                                    Rep_State = "Failed";
                                    result.value1 = "NCHL";
                                    result.value2 = "Validation Failed";
                                    response = Request.CreateResponse<CommonResponseData>(System.Net.HttpStatusCode.BadRequest, result);

                                }

                            }
                            else
                            {
                                log.Error($"{System.DateTime.Now.ToString()}lookup-QRParse {validateissuertoNPI.message.ToString()} " + Environment.NewLine);
                                result = CommonApiMethod.AllReturnBadRequestMessage(validateissuertoNPI.message);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                Res_output = JsonConvert.SerializeObject(result);
                                result.value1 = "NCHL";
                                result.value2 = "Validation Failed";
                                response = Request.CreateResponse<CommonResponseData>(System.Net.HttpStatusCode.BadRequest, result);
                                Rep_State = "Failed";
                            }
                            if (!string.IsNullOrEmpty(validateissuertoNPI.Id))
                            {
                                var jsondata = Res_output;
                                MyPay.Models.Common.CommonHelpers commonHelpers1 = new MyPay.Models.Common.CommonHelpers();
                                string Result = commonHelpers1.GetScalarValueWithValue("update Vendor_API_Requests set Res_Khalti_State='" + Rep_State + "',Res_khalti_Status=" + Rep_status + ", Res_Output='" + Res_output + "' where Id=" + validateissuertoNPI.Id + "");

                            }
                            if (Rep_State=="Failed")
                            {
                                return response;
                            }
                        }
                        else
                        {
                            result = CommonApiMethod.AllReturnBadRequestMessage(CommonResult);
                            result.Message = "Failed";
                            response.StatusCode = HttpStatusCode.BadRequest;
                            result.value1 = "NCHL";
                            result.value2 = "Validation Failed";
                            response = Request.CreateResponse<CommonResponseData>(System.Net.HttpStatusCode.BadRequest, result);
                            return response;
                        }

                        //--------------------------------------------------------End to validate qr------------------------------------------------------------------------------------//

                        #endregion End Validate QR API


                        #region Start Payment QR API
                        //--------------------------------------------------------Start payment of qr---------------------------------------------------------------------------------//

                        var amount = dataresponse.amount.ToString("0.00");
                        var txnfee = dataresponse.transactionFee.ToString("0.00"); 
                        var interchnagefee= dataresponse.interchangeFee.ToString("0.00");
                        dataresponse.amount = Convert.ToDecimal(amount);
                        
                        if (user.qrtype.ToLower() == "static")
                        {
                            var amt =Convert.ToDecimal( user.amount).ToString("0.00");
                            dataresponse.amount = Convert.ToDecimal(amt);
                        }
                        dataresponse.transactionFee = Convert.ToDecimal(txnfee);
                        dataresponse.interchangeFee = Convert.ToDecimal(interchnagefee);
                        if (Common.ApplicationEnvironment.IsProduction == true)
                        {

                            dataresponse.encKeySerial = "77CE81B5000100013C4E";
                            dataresponse.issuerId = string.IsNullOrEmpty(dataresponse.issuerId) ? "00010013" : dataresponse.issuerId;
                            dataresponse.narration = string.IsNullOrEmpty(dataresponse.narration) ? "Hello" : dataresponse.narration;
                            dataresponse.instrument = string.IsNullOrEmpty(dataresponse.instrument) ? "WAL" : dataresponse.instrument;
                            dataresponse.debtorAccount = Common.ConnectIPs_AccountNumber;
                            dataresponse.debtorAgent = Common.ConnectIps_BankId;
                            dataresponse.debtorAgentBranch = Common.ConnectIPs_BranchId;
                        }
                        else
                        {
                            dataresponse.encKeySerial = "6dace9d30005000002a9";
                            dataresponse.issuerId = string.IsNullOrEmpty(dataresponse.issuerId) ? "00010016" : dataresponse.issuerId;
                            dataresponse.narration = string.IsNullOrEmpty(dataresponse.narration) ? "Hello" : dataresponse.narration;
                            dataresponse.instrument = string.IsNullOrEmpty(dataresponse.instrument) ? "WAL" : dataresponse.instrument;
                            dataresponse.debtorAccount = Common.ConnectIPs_AccountNumber_Staging;
                            dataresponse.debtorAgent = Common.ConnectIps_BankId_Staging;
                            dataresponse.debtorAgentBranch = Common.ConnectIPs_BranchId_Staging;
                        }
                        
                        //else if(user.qrtype.ToLower() == "dynamic")
                        //{
                        //    dataresponse.amount = user.amount;
                        //}

                        if (!string.IsNullOrEmpty(resuserdetails.MiddleName))
                        {
                            dataresponse.payerName = resuserdetails.FirstName + " " + resuserdetails.MiddleName + " " + resuserdetails.LastName;
                        }
                        else
                        {
                            dataresponse.payerName = resuserdetails.FirstName + " " + resuserdetails.LastName;
                        }

                        dataresponse.payerMobileNumber = resuserdetails.ContactNumber;
                        dataresponse.payerPanId = resuserdetails.ContactNumber;
                        user.Reference = new CommonHelpers().GenerateUniqueId();
                        

                        var payerMobileNumber = dataresponse.payerMobileNumber;
                        var payerPanId = Encrypt(dataresponse.payerMobileNumber);
                        string decryptedData = Decrypt(dataresponse.merchantPan); //decrypt merchant pan with RSA/ECB/OAEPWITHSHA-256ANDMGF1PADDING
                        if (string.IsNullOrEmpty(decryptedData))
                        {
                            result = CommonApiMethod.AllReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            result.Message = "Decryption failed";
                            result.value2 = "Payment Failed";
                            response = Request.CreateResponse<CommonResponseData>(System.Net.HttpStatusCode.BadRequest, result);
                            return response; 
                        }
                        string concatenatedData = "";
                        if (!string.IsNullOrEmpty(Convert.ToString( dataresponse.interchangeFee)))
                        {
                            concatenatedData = dataresponse.validationTraceId + "," + dataresponse.instructionId + "," + dataresponse.acquirerId + "," +
                                 decryptedData + "," + dataresponse.merchantCategoryCode + "," + Convert.ToDecimal(dataresponse.amount) + "," + Convert.ToDecimal(dataresponse.transactionFee)
                                 + "," +
                                 Convert.ToDecimal(dataresponse.interchangeFee) + "," + dataresponse.currencyCode + "," + user.merchantName + "," + dataresponse.merchantBillNo + "," +
                                 dataresponse.terminal + "," + dataresponse.issuerId
                                 + "," + dataresponse.payerName + "," + dataresponse.debtorAgent + "," + dataresponse.debtorAccount;

                        }
                        else
                        {
                            concatenatedData = dataresponse.validationTraceId + "," + dataresponse.instructionId + "," + dataresponse.acquirerId + "," +
                                 decryptedData + "," + dataresponse.merchantCategoryCode + "," + Convert.ToDecimal(dataresponse.amount) + "," + Convert.ToDecimal(dataresponse.transactionFee)
                                 + "," + dataresponse.currencyCode + "," + user.merchantName + "," + dataresponse.merchantBillNo + "," +
                                 dataresponse.terminal + "," + dataresponse.issuerId
                                 + "," + dataresponse.payerName + "," + dataresponse.debtorAgent + "," + dataresponse.debtorAccount;

                        }


                        string sha256Token = GenerateConnectIPSTokenPrivateKey(concatenatedData);  // Compute SHA-256 hash (token) to generate tokem
                        var encryptmerchantpan = Encrypt(decryptedData); //encrypt merchant pan with public key
                        if (string.IsNullOrEmpty(encryptmerchantpan))
                        {
                            result = CommonApiMethod.AllReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            result.Message = "Encryption failed";
                            result.value2 = "Payment Failed";
                            response = Request.CreateResponse<CommonResponseData>(System.Net.HttpStatusCode.BadRequest, result);
                            return response;
                        }

                        dataresponse.merchantPan = encryptmerchantpan;
                        bool IsCouponUnlocked = false;
                        string TransactionID = string.Empty;
                        MyPay.Models.Common.CommonHelpers commonHelpers = new MyPay.Models.Common.CommonHelpers();
                        string API_KEY = string.Empty;
                        string UniqueTransactionId = string.Empty;
                        string RedirectURL = string.Empty;
                        string ContactNo = string.Empty;
                        string UserName = string.Empty;
                        string OrderToken = string.Empty;
                        string Password = string.Empty;
                        string UniqueMerchantId = string.Empty;
                        Req_Vendor_API_NepalPayQR_Requests obj1 = new Req_Vendor_API_NepalPayQR_Requests();
                        obj1.DeviceCode = user.DeviceCode;
                        obj1.DeviceId = user.DeviceId;
                        obj1.PlatForm = user.PlatForm;
                        obj1.MemberId = user.MemberId;
                        obj1.Version = user.Version;
                        user.PaymentMode = "1";
                        //NepalQRAuthResponse model = Authentication(obj1, UserInput, authenticationToken); //Get Authentication
                        var nQrTxnId = string.Empty;
                        NepalPayQRModel _npqr = new NepalPayQRModel();
                        dataresponse.merchantPan = decryptedData;
                        DateTime d = DateTime.Now;
                        string dateString = d.ToString("yyyyMMddHHmmssfff");

                        dataresponse.localTransactionDateTime = dateString;
                        CommonDBResonse addbusdetail = _npqr.AddNepalPayQRDetail(dataresponse, user.MemberId, dataresponse.merchantPostalcode, dataresponse.qrString, dataresponse.payerEmailAddress, dataresponse.debtorAgentBranch, "", "");
                        //obj.merchantPan = user.merchantPan;
                        GetDataFromNepalQRPay PaymentissuertoNPI = RepKhalti.GetRequestIssuerToNPI_Payment(user.Version, user.DeviceCode,
                                                user.PlatForm, Convert.ToInt64(user.MemberId), "", model.access_token, UserInput,
                                                Convert.ToInt32(KhaltiAPIName.NepalPay_QR_Payments), dataresponse, sha256Token, encryptmerchantpan,
                                                resGetCouponsScratched, ref TransactionID, user.BankTransactionId, user.PaymentMode,
                                                UniqueTransactionId, resuserdetails, payerPanId, payerMobileNumber, authenticationToken);



                        if (!string.IsNullOrEmpty(PaymentissuertoNPI.TransactionId))
                        {
                            if (PaymentissuertoNPI.IsException == false)
                            {
                                var Paymentdataresponse = JsonConvert.DeserializeObject<PaymentResponse>(PaymentissuertoNPI.message);
                                if (Paymentdataresponse.responseCode == "000")
                                {
                                    result.status = true;
                                    result.ReponseCode = 1;
                                    result.Message = "success";
                                    response.StatusCode = HttpStatusCode.Accepted;
                                    result.TransactionUniqueId = PaymentissuertoNPI.TransactionId;
                                    result.Data = Paymentdataresponse;
                                    result.StatusCode = response.StatusCode.ToString();
                                    Res_output = JsonConvert.SerializeObject(result);
                                    Rep_State = "Success";
                                    Rep_status = "1";
                                    nQrTxnId = Paymentdataresponse.nQrTxnId;
                                    response = Request.CreateResponse<CommonResponseData>(System.Net.HttpStatusCode.OK, result);
                                }
                                else
                                {
                                    var dataresponseerror = JsonConvert.DeserializeObject<ValidateResponseError>(PaymentissuertoNPI.message);
                                    result.Message =string.IsNullOrEmpty( Paymentdataresponse.responseMessage)? Paymentdataresponse.responseDescription: Paymentdataresponse.responseMessage;
                                    result.status = Paymentdataresponse.responseCode == "000" ? true : false;
                                    result.ReponseCode = Paymentdataresponse.responseCode == "000" ? 1 : 0;
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    result.Data = PaymentissuertoNPI.message;
                                    result.StatusCode = response.StatusCode.ToString();
                                    Res_output = JsonConvert.SerializeObject(result);
                                    Rep_State = "Failed";
                                    result.value2 = "Payment Failed";
                                    response = Request.CreateResponse<CommonResponseData>(System.Net.HttpStatusCode.BadRequest, result);

                                }

                            }
                            else
                            {
                                log.Error($"{System.DateTime.Now.ToString()} payment/issuer-to-NPI {PaymentissuertoNPI.message.ToString()} " + Environment.NewLine);
                                result = CommonApiMethod.AllReturnBadRequestMessage(CommonResult);
                                result.Message = "Exception";
                                response.StatusCode = HttpStatusCode.BadRequest;
                                Res_output = JsonConvert.SerializeObject(result);
                                result.value2 = "Payment Failed";
                                response = Request.CreateResponse<CommonResponseData>(System.Net.HttpStatusCode.BadRequest, result);
                                Rep_State = "Failed";
                            }

                            string TransactionId = PaymentissuertoNPI.TransactionId;
                            //BusSewaModel _bus = new BusSewaModel();
                            if (!string.IsNullOrEmpty(PaymentissuertoNPI.TransactionId))
                            {

                                string Result = commonHelpers.GetScalarValueWithValue(" update  NepalPayQR  set UpdatedDate = GetDate(),transactionStatus = '" + Rep_State + "', WalletTransactionId = '" + TransactionId + "' where  instructionId='" + dataresponse.instructionId + "'");
                            }



                            Hashtable HTs = new Hashtable();
                            HTs.Add("flag", "get");
                            HTs.Add("Id", TransactionId);
                            DataTable dt = new DataTable();
                            dt = commonHelpers.GetDataFromStoredProcedure("sp_NepalPayQR_Detail", HTs);
                            Receipt receipt = new Receipt();
                            if (dt.Rows.Count > 0)
                            {

                                DataRow row = dt.Rows[0];
                                var code = !string.IsNullOrEmpty(row["code"].ToString()) ? row["code"].ToString() : "0";
                                if (code == "1")
                                {
                                    receipt.TransactionDate = !string.IsNullOrEmpty(row["TransactionDate"].ToString()) ? row["TransactionDate"].ToString() : "";
                                    receipt.ServiceCharge = !string.IsNullOrEmpty(row["ServiceCharge"].ToString()) ? row["ServiceCharge"].ToString() : "0.00";
                                    receipt.Type = !string.IsNullOrEmpty(row["Type"].ToString()) ? row["Type"].ToString() : "";
                                    receipt.FirstName = !string.IsNullOrEmpty(row["FirstName"].ToString()) ? row["FirstName"].ToString() : "";
                                    receipt.LastName = !string.IsNullOrEmpty(row["LastName"].ToString()) ? row["LastName"].ToString() : "";
                                    receipt.MiddleName = !string.IsNullOrEmpty(row["MiddleName"].ToString()) ? row["MiddleName"].ToString() : "";
                                    receipt.Amount = !string.IsNullOrEmpty(row["Amount"].ToString()) ? row["Amount"].ToString() : "0.00";
                                    receipt.PaymentStatus = !string.IsNullOrEmpty(row["Status"].ToString()) ? row["Status"].ToString() : "";
                                }

                            }
                            var transactionFee = string.IsNullOrEmpty(Convert.ToString(dataresponse.transactionFee)) ? "0.00" : Convert.ToString(dataresponse.transactionFee);
                            var interchangeFee = string.IsNullOrEmpty(Convert.ToString(dataresponse.interchangeFee)) ? "0.00" : Convert.ToString(dataresponse.interchangeFee);
                            var list = new List<KeyValuePair<String, String>>();
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Date", receipt.TransactionDate);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Service", "NCHL QR Payment");
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "MyPay Txn Id", TransactionId);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "Instruction Id", dataresponse.instructionId);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "Payer Name", dataresponse.payerName);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "Payer Mobile Number", dataresponse.payerMobileNumber);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "Merchant Name", dataresponse.merchantName);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "Merchant City", dataresponse.merchantCity);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "Merchant BillNo", dataresponse.merchantBillNo);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Fee", "Rs. " + transactionFee);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "Interchange Fee", "Rs. " + interchangeFee);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Reference", dataresponse.merchantTxnRef);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "QR Type", dataresponse.qrType);
                            //VendorApi_CommonHelper.addKeyValueToList(ref list, "Departure Time", receipt.Time);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Status", receipt.PaymentStatus);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "Service Charge(RED)", receipt.ServiceCharge);
                            VendorApi_CommonHelper.addKeyValueToList(ref list, "Paid(RED)", receipt.Amount);
                            string JSONForReceipt = VendorApi_CommonHelper.getJSONfromList(list);
                            VendorApi_CommonHelper.saveReceipt(receipt.Type.ToString(),
                                "NCHL QR Payment", user.MemberId, TransactionId, JSONForReceipt,
                                receipt.userContact, receipt.FirstName + " " + receipt.MiddleName + " " + receipt.LastName,
                                "Payment", dataresponse.instructionId, receipt.Amount.ToString());

                        }

                        else
                        {
                            result.Message = PaymentissuertoNPI.TransactionId;
                            result.status = false;
                            result.ReponseCode = 0;
                            response.StatusCode = HttpStatusCode.BadRequest;
                            result.Data = PaymentissuertoNPI.message;
                            result.StatusCode = response.StatusCode.ToString();
                            Res_output = JsonConvert.SerializeObject(result);
                            Rep_State = "Failed";
                            //result.value2 = "Payment Failed";
                            response = Request.CreateResponse<CommonResponseData>(System.Net.HttpStatusCode.BadRequest, result);
                        }
                        if (!string.IsNullOrEmpty(PaymentissuertoNPI.Id))
                        {
                            var jsondata = Res_output;
                            string Result = commonHelpers.GetScalarValueWithValue("update Vendor_API_Requests set Res_Khalti_State='" + Rep_State + "',Res_khalti_Status=" + Rep_status + ",Res_Output='" + jsondata + "',Res_Khalti_Id='"+ nQrTxnId + "' where Id=" + PaymentissuertoNPI.Id + "");
                        }
                        //--------------------------------------------------------End payment of qr---------------------------------------------------------------------------//
                        #endregion End Payment QR API
                    }

                    log.Info($"{System.DateTime.Now.ToString()} payment/issuer-to-NPI-payment completed" + Environment.NewLine);
                    return response;
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
                //throw new CustomResponseException(false, "Bad Request", 0, "Exception Error", "", "");
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} payment/issuer-to-NPI-payment {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //[HttpPost]
        //[Route("api/payment/issuer-to-NPI-payment2")]
        //public HttpResponseMessage GetLookupService_NepalPayQR_Payment(NPITOAcquierRequest user)
        //{
        //    log.Info($"{System.DateTime.Now.ToString()} inside payment/issuer-to-NPI-payment" + Environment.NewLine);

        //    string UserInput = getRawPostData().Result;
        //    //CommonResponse cres = new CommonResponse();
        //    commonresponsedata result = new commonresponsedata();
        //    var response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
        //    if (Common.ApplicationEnvironment.IsProduction == true)
        //    {
        //        user.debtorAccount = Common.ConnectIPs_AccountNumber;
        //        user.debtorAgent = Common.ConnectIps_BankId;
        //        user.debtorAgentBranch = Common.ConnectIPs_BranchId;
        //    }
        //    else
        //    {
        //        user.debtorAccount = Common.ConnectIPs_AccountNumber_Staging;
        //        user.debtorAgent = Common.ConnectIps_BankId_Staging;
        //        user.debtorAgentBranch = Common.ConnectIPs_BranchId_Staging;
        //    }
        //    try
        //    {
        //        if (Request.Headers.Authorization == null)
        //        {
        //            string results = "Un-Authorized Request";
        //            result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
        //            response.StatusCode = HttpStatusCode.BadRequest;
        //            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
        //            return response;
        //        }
        //        else
        //        {
        //            string md5hash = Common.getHashMD5(UserInput); //Common.CheckHash(user);
        //            string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
        //            if (results != "Success")
        //            {
        //                result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
        //                response.StatusCode = HttpStatusCode.BadRequest;
        //                response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
        //                return response;
        //            }
        //            else
        //            {

        //                string CommonResult = "";
        //                AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
        //                AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

        //                Int64 memId = Convert.ToInt64(user.MemberId);
        //                int VendorAPIType = 0;
        //                int Type = 0;
        //                resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, true, Convert.ToString(user.amount), true, user.Mpin, "", false);
        //                //resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", user.Reference, user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, true, "0", true, user.Mpin, "", false);
        //                if (CommonResult.ToLower() != "success")
        //                {
        //                    result = CommonApiMethod.BusSewaReturnBadRequestMessage(CommonResult);
        //                    response.StatusCode = HttpStatusCode.BadRequest;
        //                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
        //                    return response;
        //                }
        //                if (!string.IsNullOrEmpty(resuserdetails.MiddleName))
        //                {
        //                    user.payerName = resuserdetails.FirstName + " " + resuserdetails.MiddleName + " " + resuserdetails.LastName;
        //                }
        //                else
        //                {
        //                    user.payerName = resuserdetails.FirstName + " " + resuserdetails.LastName;
        //                }

        //                user.payerMobileNumber = resuserdetails.ContactNumber;
        //                user.payerPanId = resuserdetails.ContactNumber;
        //                user.Reference = new CommonHelpers().GenerateUniqueId();
        //                string authenticationToken = Request.Headers.Authorization.Parameter;
        //                Common.authenticationToken = authenticationToken;
        //                string Res_output = response.ToString();
        //                var Rep_State = string.Empty;
        //                var Rep_status = "0";
        //                ValidateResponse obj = new ValidateResponse();
        //                obj.validationTraceId = user.validationTraceId;
        //                obj.instructionId = user.instructionId;
        //                obj.qrType = user.qrType;
        //                obj.acquirerId = user.acquirerId;
        //                obj.acquirerCountryCode = user.acquirerCountryCode;
        //                obj.merchantPan = user.merchantPan;
        //                obj.merchantCategoryCode = user.merchantCategoryCode;
        //                obj.amount = Convert.ToDecimal(user.amount);
        //                obj.transactionFee = Convert.ToDecimal(user.transactionFee);
        //                obj.currencyCode = user.currencyCode;
        //                obj.merchantName = user.merchantName;
        //                obj.merchantCity = user.merchantCity;
        //                obj.merchantCountryCode = user.merchantCountryCode;
        //                obj.merchantBillNo = user.merchantBillNo;
        //                obj.merchantTxnRef = user.merchantTxnRef;
        //                obj.merchantPostalcode = user.merchantPostalcode;
        //                obj.terminal = user.terminal;
        //                obj.qrString = user.qrString;
        //                obj.encKeySerial = user.encKeySerial;
        //                obj.network = user.network;
        //                obj.token = user.token;
        //                obj.tokenString = user.tokenString;
        //                obj.interchangeFee = Convert.ToDecimal(user.interchangeFee);
        //                obj.issuerId = user.issuerId;
        //                obj.issuerName = user.issuerName;
        //                obj.debtorAgent = user.debtorAgent;
        //                obj.debtorAccount = user.debtorAccount;
        //                obj.payerEmailAddress = user.payerEmailAddress;
        //                obj.payerMobileNumber = user.payerMobileNumber;
        //                obj.payerPanId = user.payerPanId;
        //                obj.payerName = user.payerName;
        //                obj.debtorAgentBranch = user.debtorAgentBranch;
        //                obj.narration = user.narration;
        //                obj.instrument = user.instrument;
        //                obj.localTransactionDateTime = user.localTransactionDateTime;


        //                var payerMobileNumber = user.payerMobileNumber;
        //                var payerPanId = Encrypt(user.payerMobileNumber);
        //                string decryptedData = Decrypt(user.merchantPan); //decrypt merchant pan with RSA/ECB/OAEPWITHSHA-256ANDMGF1PADDING
        //                if (string.IsNullOrEmpty(decryptedData))
        //                {
        //                    result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
        //                    response.StatusCode = HttpStatusCode.BadRequest;
        //                    result.Message = "Decryption failed";
        //                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
        //                    return response;
        //                }
        //                string concatenatedData = "";
        //                if (!string.IsNullOrEmpty(user.interchangeFee))
        //                {
        //                    concatenatedData = user.validationTraceId + "," + user.instructionId + "," + user.acquirerId + "," +
        //                         decryptedData + "," + user.merchantCategoryCode + "," + Convert.ToDecimal(user.amount) + "," + Convert.ToDecimal(user.transactionFee)
        //                         + "," +
        //                         Convert.ToDecimal(user.interchangeFee) + "," + user.currencyCode + "," + user.merchantName + "," + user.merchantBillNo + "," +
        //                         user.terminal + "," + user.issuerId
        //                         + "," + user.payerName + "," + user.debtorAgent + "," + user.debtorAccount;

        //                }
        //                else
        //                {
        //                    concatenatedData = user.validationTraceId + "," + user.instructionId + "," + user.acquirerId + "," +
        //                         decryptedData + "," + user.merchantCategoryCode + "," + Convert.ToDecimal(user.amount) + "," + Convert.ToDecimal(user.transactionFee)
        //                         + "," + user.currencyCode + "," + user.merchantName + "," + user.merchantBillNo + "," +
        //                         user.terminal + "," + user.issuerId
        //                         + "," + user.payerName + "," + user.debtorAgent + "," + user.debtorAccount;

        //                }


        //                string sha256Token = GenerateConnectIPSTokenPrivateKey(concatenatedData);  // Compute SHA-256 hash (token) to generate tokem

        //                var encryptmerchantpan = Encrypt(decryptedData); //encrypt merchant pan with public key
        //                if (string.IsNullOrEmpty(encryptmerchantpan))
        //                {
        //                    result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
        //                    response.StatusCode = HttpStatusCode.BadRequest;
        //                    result.Message = "Encryption failed";
        //                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
        //                    return response;
        //                }
        //                user.merchantPan = encryptmerchantpan;
        //                bool IsCouponUnlocked = false;
        //                string TransactionID = string.Empty;
        //                MyPay.Models.Common.CommonHelpers commonHelpers = new MyPay.Models.Common.CommonHelpers();
        //                string API_KEY = string.Empty;
        //                string UniqueTransactionId = string.Empty;
        //                string RedirectURL = string.Empty;
        //                string ContactNo = string.Empty;
        //                string UserName = string.Empty;
        //                string OrderToken = string.Empty;
        //                string Password = string.Empty;
        //                string UniqueMerchantId = string.Empty;
        //                Req_Vendor_API_NepalPayQR_Requests obj1 = new Req_Vendor_API_NepalPayQR_Requests();
        //                obj1.DeviceCode = user.DeviceCode;
        //                obj1.DeviceId = user.DeviceId;
        //                obj1.PlatForm = user.PlatForm;
        //                obj1.MemberId = user.MemberId;
        //                obj1.Version = user.Version;
        //                user.PaymentMode = "1";
        //                NepalQRAuthResponse model = Authentication(obj1, UserInput, authenticationToken); //Get Authentication
        //                var nQrTxnId = string.Empty;
        //                NepalPayQRModel _npqr = new NepalPayQRModel();
        //                obj.merchantPan = decryptedData;
        //                CommonDBResonse addbusdetail = _npqr.AddNepalPayQRDetail(obj, user.MemberId, obj.merchantPostalcode, obj.qrString, obj.payerEmailAddress, obj.debtorAgentBranch, "", "");
        //                obj.merchantPan = user.merchantPan;
        //                GetDataFromNepalQRPay PaymentissuertoNPI = RepKhalti.GetRequestIssuerToNPI_Payment(user.Version, user.DeviceCode,
        //                                         user.PlatForm, Convert.ToInt64(user.MemberId), "", model.access_token, UserInput,
        //                                         Convert.ToInt32(KhaltiAPIName.NepalPay_QR_Payments), obj, sha256Token, encryptmerchantpan,
        //                                         resGetCouponsScratched, ref TransactionID, user.BankTransactionId, user.PaymentMode,
        //                                         UniqueTransactionId, resuserdetails, payerPanId, payerMobileNumber, authenticationToken);

        //                if (!string.IsNullOrEmpty(PaymentissuertoNPI.TransactionId))
        //                {
        //                    if (PaymentissuertoNPI.IsException == false)
        //                    {
        //                        var dataresponse = JsonConvert.DeserializeObject<PaymentResponse>(PaymentissuertoNPI.message);
        //                        if (dataresponse.responseCode == "000")
        //                        {
        //                            result.status = true;
        //                            result.ReponseCode = 1;
        //                            result.Message = "success";
        //                            response.StatusCode = HttpStatusCode.Accepted;
        //                            result.TransactionUniqueId = PaymentissuertoNPI.TransactionId;
        //                            result.Data = dataresponse;
        //                            result.StatusCode = response.StatusCode.ToString();
        //                            Res_output = JsonConvert.SerializeObject(result);
        //                            Rep_State = "Success";
        //                            Rep_status = "1";
        //                            nQrTxnId = dataresponse.nQrTxnId;
        //                            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.OK, result);
        //                        }
        //                        else
        //                        {
        //                            var dataresponseerror = JsonConvert.DeserializeObject<ValidateResponseError>(PaymentissuertoNPI.message);
        //                            result.Message = dataresponse.responseMessage;
        //                            result.status = dataresponse.responseCode == "000" ? true : false;
        //                            result.ReponseCode = dataresponse.responseCode == "000" ? 1 : 0;
        //                            response.StatusCode = HttpStatusCode.BadRequest;
        //                            result.Data = PaymentissuertoNPI.message;
        //                            result.StatusCode = response.StatusCode.ToString();
        //                            Res_output = JsonConvert.SerializeObject(result);
        //                            Rep_State = "Failed";
        //                            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);

        //                        }

        //                    }
        //                    else
        //                    {
        //                        log.Error($"{System.DateTime.Now.ToString()} payment/issuer-to-NPI {PaymentissuertoNPI.message.ToString()} " + Environment.NewLine);
        //                        result = CommonApiMethod.BusSewaReturnBadRequestMessage(PaymentissuertoNPI.message);
        //                        response.StatusCode = HttpStatusCode.BadRequest;
        //                        Res_output = JsonConvert.SerializeObject(result);
        //                        response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
        //                        Rep_State = "Failed";
        //                    }

        //                    string TransactionId = PaymentissuertoNPI.TransactionId;
        //                    //BusSewaModel _bus = new BusSewaModel();
        //                    if (!string.IsNullOrEmpty(PaymentissuertoNPI.TransactionId))
        //                    {

        //                        string Result = commonHelpers.GetScalarValueWithValue(" update  NepalPayQR  set UpdatedDate = GetDate(),transactionStatus = '" + Rep_State + "', WalletTransactionId = '" + TransactionId + "' where  instructionId='" + user.instructionId + "'");
        //                    }



        //                    Hashtable HTs = new Hashtable();
        //                    HTs.Add("flag", "get");
        //                    HTs.Add("Id", TransactionId);
        //                    DataTable dt = new DataTable();
        //                    dt = commonHelpers.GetDataFromStoredProcedure("sp_NepalPayQR_Detail", HTs);
        //                    Receipt receipt = new Receipt();
        //                    if (dt.Rows.Count > 0)
        //                    {

        //                        DataRow row = dt.Rows[0];
        //                        var code = !string.IsNullOrEmpty(row["code"].ToString()) ? row["code"].ToString() : "0";
        //                        if (code == "1")
        //                        {
        //                            receipt.TransactionDate = !string.IsNullOrEmpty(row["TransactionDate"].ToString()) ? row["TransactionDate"].ToString() : "";
        //                            receipt.ServiceCharge = !string.IsNullOrEmpty(row["ServiceCharge"].ToString()) ? row["ServiceCharge"].ToString() : "0.00";
        //                            receipt.Type = !string.IsNullOrEmpty(row["Type"].ToString()) ? row["Type"].ToString() : "";
        //                            receipt.FirstName = !string.IsNullOrEmpty(row["FirstName"].ToString()) ? row["FirstName"].ToString() : "";
        //                            receipt.LastName = !string.IsNullOrEmpty(row["LastName"].ToString()) ? row["LastName"].ToString() : "";
        //                            receipt.MiddleName = !string.IsNullOrEmpty(row["MiddleName"].ToString()) ? row["MiddleName"].ToString() : "";
        //                            receipt.Amount = !string.IsNullOrEmpty(row["Amount"].ToString()) ? row["Amount"].ToString() : "0.00";
        //                            receipt.PaymentStatus = !string.IsNullOrEmpty(row["Status"].ToString()) ? row["Status"].ToString() : "";
        //                        }

        //                    }
        //                    var transactionFee = string.IsNullOrEmpty(Convert.ToString(user.transactionFee)) ? "0.00" : Convert.ToString(user.transactionFee);
        //                    var interchangeFee = string.IsNullOrEmpty(Convert.ToString(user.interchangeFee)) ? "0.00" : Convert.ToString(user.interchangeFee);
        //                    var list = new List<KeyValuePair<String, String>>();
        //                    VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Date", receipt.TransactionDate);
        //                    VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Service", "NCHL QR Payment");
        //                    VendorApi_CommonHelper.addKeyValueToList(ref list, "MyPay Txn Id", TransactionId);
        //                    VendorApi_CommonHelper.addKeyValueToList(ref list, "Instruction Id", user.instructionId);
        //                    VendorApi_CommonHelper.addKeyValueToList(ref list, "Payer Name", user.payerName);
        //                    VendorApi_CommonHelper.addKeyValueToList(ref list, "Payer Mobile Number", user.payerMobileNumber);
        //                    VendorApi_CommonHelper.addKeyValueToList(ref list, "Merchant Name", user.merchantName);
        //                    VendorApi_CommonHelper.addKeyValueToList(ref list, "Merchant City", user.merchantCity);
        //                    VendorApi_CommonHelper.addKeyValueToList(ref list, "Merchant BillNo", user.merchantBillNo);
        //                    VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Fee", "Rs. " + transactionFee);
        //                    VendorApi_CommonHelper.addKeyValueToList(ref list, "Interchange Fee", "Rs. " + interchangeFee);
        //                    VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Reference", user.merchantTxnRef);
        //                    VendorApi_CommonHelper.addKeyValueToList(ref list, "QR Type", user.qrType);
        //                    //VendorApi_CommonHelper.addKeyValueToList(ref list, "Departure Time", receipt.Time);
        //                    VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Status", receipt.PaymentStatus);
        //                    VendorApi_CommonHelper.addKeyValueToList(ref list, "Service Charge(RED)", receipt.ServiceCharge);
        //                    VendorApi_CommonHelper.addKeyValueToList(ref list, "Paid(RED)", receipt.Amount);
        //                    string JSONForReceipt = VendorApi_CommonHelper.getJSONfromList(list);
        //                    VendorApi_CommonHelper.saveReceipt(receipt.Type.ToString(),
        //                        "NCHL QR Payment", user.MemberId, TransactionId, JSONForReceipt,
        //                        receipt.userContact, receipt.FirstName + " " + receipt.MiddleName + " " + receipt.LastName,
        //                        "Payment", user.instructionId, receipt.Amount.ToString());

        //                }

        //                else
        //                {
        //                    result.Message = PaymentissuertoNPI.TransactionId;
        //                    result.status = false;
        //                    result.ReponseCode = 0;
        //                    response.StatusCode = HttpStatusCode.BadRequest;
        //                    result.Data = PaymentissuertoNPI.message;
        //                    result.StatusCode = response.StatusCode.ToString();
        //                    Res_output = JsonConvert.SerializeObject(result);
        //                    Rep_State = "Failed";
        //                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
        //                }
        //                if (!string.IsNullOrEmpty(PaymentissuertoNPI.Id))
        //                {
        //                    var jsondata = Res_output;
        //                    string Result = commonHelpers.GetScalarValueWithValue("update Vendor_API_Requests set Res_Khalti_State='" + Rep_State + "',Res_khalti_Status=" + Rep_status + ",Res_Output='" + jsondata + "' where Id=" + PaymentissuertoNPI.Id + "");
        //                }

        //            }

        //            log.Info($"{System.DateTime.Now.ToString()} payment/issuer-to-NPI-payment completed" + Environment.NewLine);
        //            return response;
        //        }
        //    }
        //    catch (DbEntityValidationException e)
        //    {
        //        foreach (var eve in e.EntityValidationErrors)
        //        {
        //            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
        //                eve.Entry.Entity.GetType().Name, eve.Entry.State);
        //            foreach (var ve in eve.ValidationErrors)
        //            {
        //                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
        //                    ve.PropertyName, ve.ErrorMessage);
        //            }
        //        }
        //        //throw new CustomResponseException(false, "Bad Request", 0, "Exception Error", "", "");
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error($"{System.DateTime.Now.ToString()} payment/issuer-to-NPI-payment {ex.ToString()} " + Environment.NewLine);
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}

        [HttpPost]
        [Route("api/NPI-to-issuer-refund")]
        public HttpResponseMessage GetLookupService_NepalPayQR_Refund(NepalQRRefundRequest user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside validate/issuer-to-NPI-refund" + Environment.NewLine);
            string UserInput = getRawPostData().Result;
            RefundResponseFromIssuerToNPI result = new RefundResponseFromIssuerToNPI();
            var response = Request.CreateResponse<RefundResponseFromIssuerToNPI>(System.Net.HttpStatusCode.BadRequest, result);
            string DeviceCode = new DeviceIdBuilder().AddMachineName().AddProcessorId().AddMotherboardSerialNumber().AddSystemDriveSerialNumber().ToString();
            string Version = "";
            string AuthorizationKey = "";
            string OldInsId = "";
            Int64 MemberId = 0;
            var ip = GetIp();

//--------------------------------------- Start to verify basic authentication -----------------------------------------------//
            var request = HttpContext.Current.Request;
            var authHeader = request.Headers["Authorization"];
            if (authHeader != null)
            {
                var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);
                // RFC 2617 sec 1.2, "scheme" name is case-insensitive
                if (authHeaderVal.Scheme.Equals("basic",
                        StringComparison.OrdinalIgnoreCase) &&
                    authHeaderVal.Parameter != null)
                {
                    var check = BasicAuthenticateUser(authHeaderVal.Parameter);
                    if (check == "false")
                    {
                        result.responseCode = "001";
                        result.responseMessage = "UnAuthorized access";
                        response = Request.CreateResponse<RefundResponseFromIssuerToNPI>(System.Net.HttpStatusCode.BadRequest, result);
                        return response;
                    }
                }
            }
            else
            {
                result.responseCode = "001";
                result.responseMessage = "UnAuthorized authentication";
                response = Request.CreateResponse<RefundResponseFromIssuerToNPI>(System.Net.HttpStatusCode.BadRequest, result);
                return response;
            }


//------------------------------------ End  Basic Authentication --------------------------------------//
            if (string.IsNullOrEmpty(user.orgnNQrTxnId))
            {
                result.responseCode = "001";
                result.responseMessage = "orgnNQrTxnId is required";
                response = Request.CreateResponse<RefundResponseFromIssuerToNPI>(System.Net.HttpStatusCode.BadRequest, result);
                return response;
            }
            else if (string.IsNullOrEmpty(user.issuerId))
            {
                result.responseCode = "001";
                result.responseMessage = "orgnNQrTxnId is required";
                response = Request.CreateResponse<RefundResponseFromIssuerToNPI>(System.Net.HttpStatusCode.BadRequest, result);
                return response;
            }
            else if (string.IsNullOrEmpty(user.refundType))
            {
                result.responseCode = "001";
                result.responseMessage = "orgnNQrTxnId is required";
                response = Request.CreateResponse<RefundResponseFromIssuerToNPI>(System.Net.HttpStatusCode.BadRequest, result);
                return response;
            }
            else if (string.IsNullOrEmpty(user.amount))
            {
                result.responseCode = "001";
                result.responseMessage = "orgnNQrTxnId is required";
                response = Request.CreateResponse<RefundResponseFromIssuerToNPI>(System.Net.HttpStatusCode.BadRequest, result);
                return response;
            }
            else if (string.IsNullOrEmpty(user.instructionId))
            {
                result.responseCode = "001";
                result.responseMessage = "Instruction Id is required";
                response = Request.CreateResponse<RefundResponseFromIssuerToNPI>(System.Net.HttpStatusCode.BadRequest, result);
                return response;
            }
            else if (string.IsNullOrEmpty(user.token))
            {
                result.responseCode = "001";
                result.responseMessage = "Token is required";
                response = Request.CreateResponse<RefundResponseFromIssuerToNPI>(System.Net.HttpStatusCode.BadRequest, result);
                return response;
            }
            else if (string.IsNullOrEmpty(user.transactionFee))
            {
                result.responseCode = "001";
                result.responseMessage = "Transaction Fee is required";
                response = Request.CreateResponse<RefundResponseFromIssuerToNPI>(System.Net.HttpStatusCode.BadRequest, result);
                return response;
            }
            else if (string.IsNullOrEmpty(user.refundCancellationFlg))
            {
                result.responseCode = "001";
                result.responseMessage = "Refund cancellation flag is required";
                response = Request.CreateResponse<RefundResponseFromIssuerToNPI>(System.Net.HttpStatusCode.BadRequest, result);
                return response;
            }
            else if (string.IsNullOrEmpty(user.refundNQrTxnId))
            {
                result.responseCode = "001";
                result.responseMessage = "Refund txn id is required";
                response = Request.CreateResponse<RefundResponseFromIssuerToNPI>(System.Net.HttpStatusCode.BadRequest, result);
                return response;
            }
            else if (string.IsNullOrEmpty(user.payerPanId))
            {
                result.responseCode = "001";
                result.responseMessage = "Payer pan id is required";
                response = Request.CreateResponse<RefundResponseFromIssuerToNPI>(System.Net.HttpStatusCode.BadRequest, result);
                return response;
            }

            else if (string.IsNullOrEmpty(user.payerMobileNumber))
            {
                result.responseCode = "001";
                result.responseMessage = "Payer mobile number is required";
                response = Request.CreateResponse<RefundResponseFromIssuerToNPI>(System.Net.HttpStatusCode.BadRequest, result);
                return response;
            }
            else if (string.IsNullOrEmpty(user.refundReasonCode))
            {
                result.responseCode = "001";
                result.responseMessage = "Refund reason code is required";
                response = Request.CreateResponse<RefundResponseFromIssuerToNPI>(System.Net.HttpStatusCode.BadRequest, result);
                return response;
            }
            decimal addedtransactionamount = Convert.ToDecimal(user.amount);
            string decryptedData = Decrypt(user.payerPanId); //decrypt merchant pan with RSA/ECB/OAEPWITHSHA-256ANDMGF1PADDING

            if (!string.IsNullOrEmpty(user.transactionFee))
            {
                if (Convert.ToDecimal( user.transactionFee)>0)
                {
                    addedtransactionamount = addedtransactionamount + Convert.ToDecimal(user.transactionFee);
                }
            }
//-------------------------------- Start used for generating token to verify token of NCHL rquest --------------------------------------------//
            string concatenatedData = user.orgnNQrTxnId + "," + user.issuerId + "," + Convert.ToDecimal(user.amount) + "," + user.refundReasonCode + "," + user.instructionId + "," +
                                 user.refundCancellationFlg + "," + user.refundNQrTxnId + "," + decryptedData;

            string sha256Token = GenerateConnectIPSTokenPrivateKey(concatenatedData);  // Compute SHA-256 hash (token) to generate token
            user.token = sha256Token;

            bool ValidateSignature = Common.VerifyConnectIPSToken_LinkBank(concatenatedData, user.token);
            Common.AddLogs($"VerifyNCHLRefundToken:{ValidateSignature}", false, (int)AddLog.LogType.DBLogs);
            if (ValidateSignature == false)
            {
                result.responseCode = "001";
                result.responseMessage = "Invalid Token";
                response = Request.CreateResponse<RefundResponseFromIssuerToNPI>(System.Net.HttpStatusCode.BadRequest, result);
                return response;
            }

//--------------------------------- End sed for generating token to verify token of NCHL rquest ----------------------------------------------------//
            
            Hashtable HTs = new Hashtable();
            HTs.Add("flag", "refundget");
            HTs.Add("nQrTxnId", user.orgnNQrTxnId);
            DataTable dt = new DataTable();
            MyPay.Models.Common.CommonHelpers commonHelpers = new MyPay.Models.Common.CommonHelpers();
            dt = commonHelpers.GetDataFromStoredProcedure("sp_NepalPayQR_Detail", HTs);
            string memberId = string.Empty;
            string OrginalWalletTransactionId = string.Empty;
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                var code = !string.IsNullOrEmpty(row["code"].ToString()) ? row["code"].ToString() : "0";
                var message = !string.IsNullOrEmpty(row["message"].ToString()) ? row["message"].ToString() : "";
                if (code == "1")
                {
                    memberId = !string.IsNullOrEmpty(row["MemberId"].ToString()) ? row["MemberId"].ToString() : "0";
                    OldInsId = !string.IsNullOrEmpty(row["ins"].ToString()) ? row["ins"].ToString() : "";
                    OrginalWalletTransactionId = !string.IsNullOrEmpty(row["WalletTransactionId"].ToString()) ? row["WalletTransactionId"].ToString() : "0";

                }
                else
                {
                    result.responseCode = "001";
                    result.responseMessage = message;
                    response = Request.CreateResponse<RefundResponseFromIssuerToNPI>(System.Net.HttpStatusCode.BadRequest, result);
                    return response;
                }
            }
            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.NepalPay_QR_Payments;
            int VendorType = (int)VendorApi_CommonHelper.VendorTypes.NCHLQR;
            //string JsonReq = VendorApi_CommonHelper.GenerateApi_Input_JsonRequest(Reference, "", "", string.Empty, VendorAPIType);
            string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(VendorAPIType)).ToString().ToUpper().Replace("_", " ");
            string KhaltiAPIURL = "";
            var data = JsonConvert.SerializeObject(user);
            var token = "";
             KhaltiAPIURL = Convert.ToString( Request.RequestUri);
            var objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse("", "", MemberId, "", data, token, UserInput, DeviceCode, "web", VendorAPIType, "", "", "", Convert.ToString(VendorType), 0);
            var Id = Convert.ToString(objVendor_API_Requests.Id);
         
            try
            {
                string Res_output = string.Empty;
                string Rep_State = string.Empty;
                string Rep_status = string.Empty;
               
                string authenticationToken = Request.Headers.Authorization.Parameter;
                //Common.authenticationToken = authenticationToken;

                NepalPayQRModel _npqr = new NepalPayQRModel();
                RefundResponseFromIssuerToNPI obj = new RefundResponseFromIssuerToNPI();
                obj.orgnNQrTxnId = user.orgnNQrTxnId;
                obj.instructionId = user.instructionId;
                obj.amount = Convert.ToDecimal(user.amount);
                obj.refundType = user.refundType;
                obj.issuerId = user.issuerId;
                obj.transactionFee = user.transactionFee;
                obj.refundReasonMessage = user.refundReasonMessage;
                obj.payerPanId =decryptedData;
                obj.payerMobileNumber = user.payerMobileNumber;
                obj.refundNQrTxnId = user.refundNQrTxnId;




 //------------------------- Start To check refund amount is correct or not  ------------------------------//
                 
                Hashtable HT2 = new Hashtable();
                HT2.Add("flag", "checkrefundamount");
                HT2.Add("instructionId", user.instructionId);
                HT2.Add("Amount", addedtransactionamount);
                HT2.Add("RefundType", user.refundType);
                HT2.Add("OrginalTxnId", user.orgnNQrTxnId);
                DataTable dt2 = new DataTable();
                string RefundCode = "0";
                string Refundmessage = "";
                MyPay.Models.Common.CommonHelpers commonHelpers1 = new MyPay.Models.Common.CommonHelpers();
                dt2 = commonHelpers1.GetDataFromStoredProcedure("sp_NepalPayQR_Detail", HT2);   //--Check refund amount exceed more than actual amount or not --//
                if (dt2.Rows.Count > 0)
                {
                    DataRow row = dt2.Rows[0];
                    RefundCode = !string.IsNullOrEmpty(row["code"].ToString()) ? row["code"].ToString() : "0";
                    Refundmessage = !string.IsNullOrEmpty(row["message"].ToString()) ? row["message"].ToString() : "";
                    if (RefundCode == "0")
                    {
                        result.responseCode = "001";
                        result.responseMessage = Refundmessage;
                        response = Request.CreateResponse<RefundResponseFromIssuerToNPI>(System.Net.HttpStatusCode.BadRequest, result);
                        return response;
                    }
                }
                else
                {
                    result.responseCode = "001";
                    result.responseMessage = "Data not found.";
                    response = Request.CreateResponse<RefundResponseFromIssuerToNPI>(System.Net.HttpStatusCode.BadRequest, result);
                    return response;
                }

 //----------------------------- End To check refund amount is correct or not   --------------------------------------------//



//------------------------------- Start to Add data in Refund table of Nepalpay -----------------------------------------------//
                CommonDBResonse addbusdetail = _npqr.AddNepalPayQRRefund(obj, memberId, "", OrginalWalletTransactionId);
                if (addbusdetail.code == "0")
                {
                    result.responseCode = "001";
                    result.responseMessage = "Data not saved";
                    response = Request.CreateResponse<RefundResponseFromIssuerToNPI>(System.Net.HttpStatusCode.BadRequest, result);
                    return response;
                }
//---------------------------------- End to Add data in Refund table of Nepalpay -----------------------------------------------//

                string CommonResult = "";
                int Type = 0;
                AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                resuserdetails = new CommonHelpers().CheckUserDetailNCHL("", "", "", "", ref resGetCouponsScratched, "", "", ref CommonResult, Type, VendorAPIType, Convert.ToInt64(memberId), true, false, "0", false, "", "", true, false);
                //resuserdetails = new CommonHelpers().CheckUserDetail("", "", user.Reference, "", ref resGetCouponsScratched, "", "", ref CommonResult, Type, VendorAPIType, Convert.ToInt64(memberId), true, true, Convert.ToString(user.amount), false, "", "", false);

                if (CommonResult.ToLower() != "success")
                {
                    result.responseCode = "001";
                    result.responseMessage = "User detail not found";
                    response = Request.CreateResponse<RefundResponseFromIssuerToNPI>(System.Net.HttpStatusCode.BadRequest, result);
                    return response;
                }
                resuserdetails.TotalAmount = resuserdetails.TotalAmount + addedtransactionamount;
                RefundResponseFromIssuerToNPI dataresponse = new RefundResponseFromIssuerToNPI();
                if (dataresponse.responseCode != "001")
                {
                    resuserdetails.MemberId =Convert.ToInt32( memberId);
                    //resuserdetails.DeviceCode = DeviceCode;
                    //string authenticationToken = Request.Headers.Authorization.Parameter;
                    //Common.authenticationToken = authenticationToken;
                    string name =resuserdetails.ContactNumber;                                                         
                    var refundresult= Common.RefundUpdateTransactionNCHL(name,resuserdetails, objVendor_API_Requests, OrginalWalletTransactionId, VendorApiTypeName, "", VendorAPIType, "1", "NCHL", DeviceCode);
                    if (refundresult.ToLower()=="success")
                    {
                        string refundtxnid = commonHelpers.GetScalarValueWithValue("select TransactionUniqueId from WalletTransactions   where  ParentTransactionId='" + OrginalWalletTransactionId + "'");

                        if (string.IsNullOrEmpty( refundtxnid) )
                        {
                            Rep_status = "0";
                            result.responseCode = "001";
                            result.responseMessage = "Refund txn ";
                            Res_output = JsonConvert.SerializeObject(result);
                            Rep_State = "Failed";
                            response = Request.CreateResponse<RefundResponseFromIssuerToNPI>(System.Net.HttpStatusCode.BadRequest, result);
                        }
                        else
                        {
                            Rep_State = "Success";
                            Rep_status = "1";
                            dataresponse.responseCode = "000";
                            dataresponse.responseMessage = "success";
                            dataresponse.orgnNQrTxnId = user.orgnNQrTxnId;
                            dataresponse.issuerId = user.issuerId;
                            dataresponse.refundType = user.refundType;
                            dataresponse.amount = Convert.ToDecimal(user.amount);
                            dataresponse.transactionFee = user.transactionFee;
                            dataresponse.refundReasonCode = user.refundReasonCode;
                            dataresponse.refundReasonMessage = user.refundReasonMessage;
                            dataresponse.instructionId = user.instructionId;
                            dataresponse.refundCancellationFlg = user.refundCancellationFlg;
                            dataresponse.refundNQrTxnId = user.refundNQrTxnId;
                            dataresponse.payerPanId = user.payerPanId;
                            dataresponse.payerMobileNumber = user.payerMobileNumber;
                            Res_output = JsonConvert.SerializeObject(dataresponse);
                            response = Request.CreateResponse<RefundResponseFromIssuerToNPI>(System.Net.HttpStatusCode.OK, dataresponse);
                            // string refundtxnid = commonHelpers.GetScalarValueWithValue("select TransactionUniqueId from WalletTransactions   where  ParentTransactionId='" + OrginalWalletTransactionId + "'");
                            string Result1 = commonHelpers.GetScalarValueWithValue(" update  NepalPayQR_Refund  set UpdatedDate = GetDate(), MyPayRefundedTxnId = '" + refundtxnid + "',Status = '" + Rep_State + "'  where  Id='" + addbusdetail.code + "'");


                        }
                    }
                    else
                    {
                        Rep_status = "0";
                        result.responseCode = "001";
                        result.responseMessage = refundresult;
                        Res_output = JsonConvert.SerializeObject(result);
                        Rep_State = "Failed";
                        response = Request.CreateResponse<RefundResponseFromIssuerToNPI>(System.Net.HttpStatusCode.BadRequest, result);
                    }

                }
                else
                {
                    Rep_status = "0";
                    result.responseCode = "001";
                    result.responseMessage = dataresponse.responseMessage;
                    Res_output = JsonConvert.SerializeObject(result);
                    Rep_State = "Failed";
                    response = Request.CreateResponse<RefundResponseFromIssuerToNPI>(System.Net.HttpStatusCode.BadRequest, result);

                }

                if (!string.IsNullOrEmpty(Id))
                {
                    var jsondata = Res_output;
                    string Result = commonHelpers.GetScalarValueWithValue("update Vendor_API_Requests set Res_Khalti_State='" + Rep_State + "',Res_khalti_Status=" + Rep_status + ",Res_Output='" + jsondata + "' where Id=" + Id + "");

                }
                log.Info($"{System.DateTime.Now.ToString()} validate/issuer-to-NPI-refund completed" + Environment.NewLine);
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
                //throw new CustomResponseException(false, "Bad Request", 0, "Exception Error", "", "");
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} validate/issuer-to-NPI-refund {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        //[Route("api/NCHLQR-checkstatus")]
        //public HttpResponseMessage GetLookupService_NepalPayQR_CheckStatus(NepalQRCheckStatusRequest user)
        //{
        //    log.Info($"{System.DateTime.Now.ToString()} inside api/NCHLQR-checkstatus" + Environment.NewLine);
        //    string UserInput = getRawPostData().Result;
        //    //CommonResponse cres = new CommonResponse();
        //    commonresponsedata result = new commonresponsedata();
        //    var response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
        //    try
        //    {
        //        if (Request.Headers.Authorization == null)
        //        {
        //            string results = "Un-Authorized Request";
        //            result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
        //            response.StatusCode = HttpStatusCode.BadRequest;
        //            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
        //            return response;
        //        }
        //        else
        //        {
        //            string md5hash = Common.getHashMD5(UserInput); //Common.CheckHash(user);
        //            string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
        //            if (results != "Success")
        //            {
        //                result = CommonApiMethod.BusSewaReturnBadRequestMessage(results);
        //                response.StatusCode = HttpStatusCode.BadRequest;
        //                response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
        //                return response;
        //            }
        //            else
        //            {

        //                string CommonResult = "";
        //                AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
        //                AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

        //                Int64 memId = 0;
        //                int VendorAPIType = 0;
        //                int Type = 0;
        //                resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false);
        //                if (CommonResult.ToLower() != "success")
        //                {
        //                    result = CommonApiMethod.BusSewaReturnBadRequestMessage(CommonResult);
        //                    response.StatusCode = HttpStatusCode.BadRequest;
        //                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
        //                    return response;
        //                }
        //                string Reference = new CommonHelpers().GenerateUniqueId();
        //                string authenticationToken = Request.Headers.Authorization.Parameter;
        //                Common.authenticationToken = authenticationToken;
        //                string Res_output = response.ToString();
        //                var Rep_State = string.Empty;
        //                var Rep_status = "0";
        //                Req_Vendor_API_NepalPayQR_Requests obj1 = new Req_Vendor_API_NepalPayQR_Requests();
        //                obj1.DeviceCode = user.DeviceCode;
        //                obj1.DeviceId = user.DeviceId;
        //                obj1.PlatForm = user.PlatForm;
        //                obj1.MemberId = user.MemberId;
        //                obj1.Version = user.Version;
        //                user.PaymentMode = "1";
        //                NepalQRAuthResponse model = Authentication(obj1, UserInput, authenticationToken); //Get Authentication
        //                string instructionId = "MyPay" + new CommonHelpers().GenerateUniqueId_NepalPay();
        //                if (!string.IsNullOrEmpty(model.access_token))
        //                {
        //                    GetDataFromNepalQRPay validateissuertoNPI = RepKhalti.GetRequestCheckstatus(instructionId,user.validationtraceid, user.acquirerid, user.merchantid, user.nQrTxnId,  user.Version, user.DeviceCode, user.PlatForm, Convert.ToInt64(user.MemberId), "", authenticationToken, UserInput, Convert.ToInt32(KhaltiAPIName.NepalPay_QR_Payments), model.access_token);
        //                    if (validateissuertoNPI.IsException == false)
        //                    {
        //                        var dataresponse = JsonConvert.DeserializeObject<NepalQRCheckStatusResponse>(validateissuertoNPI.message);
        //                        if (dataresponse.responseCode == "000")
        //                        {
        //                            result.status = true;
        //                            result.ReponseCode = 1;
        //                            result.Message = "success";
        //                            response.StatusCode = HttpStatusCode.Accepted;
        //                            result.Data = dataresponse;
        //                            result.StatusCode = response.StatusCode.ToString();
        //                            Res_output = JsonConvert.SerializeObject(result);
        //                            Rep_State = "Success";
        //                            Rep_status = "1";
        //                            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.OK, result);
        //                        }
        //                        else
        //                        {
        //                            result.Message = dataresponse.responseMessage;
        //                            result.status = dataresponse.responseCode == "000" ? true : false;
        //                            result.ReponseCode = dataresponse.responseCode == "000" ? 1 : 0;
        //                            response.StatusCode = HttpStatusCode.BadRequest;
        //                            result.Data = validateissuertoNPI.message;
        //                            result.StatusCode = response.StatusCode.ToString();
        //                            Res_output = JsonConvert.SerializeObject(result);
        //                            Rep_State = "Failed";
        //                            response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);

        //                        }

        //                    }
        //                    else
        //                    {
        //                        log.Error($"{System.DateTime.Now.ToString()} api/NCHLQR-checkstatus {validateissuertoNPI.message.ToString()} " + Environment.NewLine);
        //                        result = CommonApiMethod.BusSewaReturnBadRequestMessage(validateissuertoNPI.message);
        //                        response.StatusCode = HttpStatusCode.BadRequest;
        //                        Res_output = JsonConvert.SerializeObject(result);
        //                        response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
        //                        Rep_State = "Failed";
        //                    }
        //                    if (!string.IsNullOrEmpty(validateissuertoNPI.Id))
        //                    {
        //                        var jsondata = Res_output;
        //                        MyPay.Models.Common.CommonHelpers commonHelpers = new MyPay.Models.Common.CommonHelpers();
        //                        string Result = commonHelpers.GetScalarValueWithValue("update Vendor_API_Requests set Res_Khalti_State='" + Rep_State + "',Res_khalti_Status=" + Rep_status + ", Res_Output='" + jsondata + "' where Id=" + validateissuertoNPI.Id + "");

        //                    }
        //                }
        //                else
        //                {
        //                    result = CommonApiMethod.BusSewaReturnBadRequestMessage(CommonResult);
        //                    response.StatusCode = HttpStatusCode.BadRequest;
        //                    response = Request.CreateResponse<commonresponsedata>(System.Net.HttpStatusCode.BadRequest, result);
        //                    return response;
        //                }


        //            }

        //        }
        //        log.Info($"{System.DateTime.Now.ToString()} api/NCHLQR-checkstatus completed" + Environment.NewLine);
        //        return response;
        //    }
        //    catch (DbEntityValidationException e)
        //    {
        //        foreach (var eve in e.EntityValidationErrors)
        //        {
        //            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
        //                eve.Entry.Entity.GetType().Name, eve.Entry.State);
        //            foreach (var ve in eve.ValidationErrors)
        //            {
        //                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
        //                    ve.PropertyName, ve.ErrorMessage);
        //            }
        //        }
        //        //throw new CustomResponseException(false, "Bad Request", 0, "Exception Error", "", "");
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error($"{System.DateTime.Now.ToString()} api/NCHLQR-checkstatus {ex.ToString()} " + Environment.NewLine);
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}
        public string GetIp()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }
        public string BasicAuthenticateUser(string credentials)
        {
            try
            {
                var encoding = Encoding.GetEncoding("iso-8859-1");
                credentials = encoding.GetString(Convert.FromBase64String(credentials));

                int separator = credentials.IndexOf(':');
                string name = credentials.Substring(0, separator);
                string password = credentials.Substring(separator + 1);

                if (CheckPassword(name, password))
                {
                    var identity = new GenericIdentity(name);
                    SetPrincipal(new GenericPrincipal(identity, null));
                    return "true";
                }
                else
                {
                    // Invalid username or password.
                    //HttpContext.Current.Response.StatusCode = 401;
                }
            }
            catch (FormatException)
            {
                // Credentials were not formatted correctly.
                //HttpContext.Current.Response.StatusCode = 401;
            }
            return "false";
        }
        public void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }
        public bool CheckPassword(string username, string password)
        {
            var user = string.Empty;
            var userpassword = string.Empty;
            if (Common.ApplicationEnvironment.IsProduction == true)
            {
                user = "mypaylivenqr";
                userpassword = "$%myp@ylive@123#";
            }
            else
            {
                user = "mypayuatnqr";
                userpassword = "Abcduat@123";
            }

            return username == user && password == userpassword;
        }
        public string Decrypt(string encryptedStr)
        {
            string originalStr = string.Empty;
            try
            {
                Chilkat.Rsa rsa1 = new Chilkat.Rsa();
                Chilkat.PrivateKey privateKey = new Chilkat.PrivateKey();
                string privateFile_NCHLQR = string.Empty;
                string PFXPassword_LinkBank = string.Empty;
                if (Common.ApplicationEnvironment.IsProduction == true)
                {
                    //privateFile_NCHLQR = RepNCHL.privateKeyLIVE_NCHLQR;
                    privateFile_NCHLQR = "-----BEGIN PRIVATE KEY-----\r\nMIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQCeiGl0d23USL9v\r\njVIzhIrWZvnDPB5DBaepMXCXv+CpH0kUdIWKvZ2EiI3g64t/ZhokAmNe8vTJhSEc\r\ndZBj37zrt/6uE9UT3/pz3lbH0GmUd4/ZsYvW/r/ycXNnD6i+QjndcALzHHl7SRW/\r\n7PKWwH9AT583DjwoLGX8bH3kdMyn63EqcYdqiGDTqqC9cV4aIGfG5ZRmGqdfycne\r\nL0J1ynszuU6zH1WHRTe6clMg8jkngJtgDvv5Ey5SmYHB+HdGcE6wkfVoYXQT6CuG\r\npfhkUOzfJrM5jgJ3gZ3DESljsGOHEH1pR8B16QSF5h9l0O9SR4lzc3sR4sJcsTbb\r\nP0x2IKBnAgMBAAECggEAD8j6YxffLjN+KD/Ujv+kCVwgg2GRi8/c1VpwYRPAbWaJ\r\nkslVYK6nqAImyl35uloywSIhvdsIJ6ajWN+V3HPzLi9YYwHjhgKXwADD+EpWsOtE\r\nvgLRLzuXZi8ItBrZjPQwKc3U5VsQ94cb7MvNjdgl84PaERPQRpnBH+S3lk9Q72k4\r\nQ+F+sB5c8oqiN1TXWRYrjb1KD0ia2PHKIroWYmxIWzRs2eOfTm1UyXHEdwNVXII9\r\nGvNw4aioo/bIk4trxjVCtrPKDY+TN/F9qoY87d0FNF5zlJl9Lv55alDJHvaxOvef\r\n1lfI9Vu2s14lstu//ZPkhXCRGB7ZJ6ZpotkeuISsJQKBgQC3UtgnHUfT/mQKG0U4\r\n/WQzeFvJ8YLi+Dg2sOkbJg1APxr08b7GkXf7fFMJqc5oihXcinEMEOhjLmskFat+\r\n5Vuf1iZwPu8F273XDEzyb7fLNHapiBMPg93ITvWcYgpzGSYFUr04OKkYcRvJR7w8\r\nF6U+uV6D1eImzxJk4x8/eYHZbQKBgQDdYZpBlGUhUzkGKvK0RJfF6kVNSXjKF1Ed\r\nnxLV3jz5YcRX5jI9KYWhgYS4tnJSCxAZMefGod05AIsB9VktlzK8n+hDiOjvgOQQ\r\nQU9f1Dwc4ovKaYJBI6HvRo2JwpeXvfjLxLh/rmxtV9x6XdArhGRr0xaInbVuR/Gq\r\nCrDeQfHwowKBgBD5/gRZRM6P70m0TsvBQ2c0zSM9V0aXf/Uomt2lv3JkpaVrQaR3\r\n0ima0MHDVNb/epjKxT89zZdptAhKbXA54ytBFJwuZ8oeGE1y2SlWSSnrONXoQQOd\r\nzYsALOSHe7o+6PNzPwyWyqn1x4HzP6487lOJrQP+aYv4fxaSpdEl+m1dAoGAI8Mv\r\noyZwy9hg3uEzQfHOvtcgiOK0k5j+rpVS+p+jI/oqOLTkKsM5ZiTBJwG6KAzHdfp8\r\n4bamQR4YVqGm3VmOhbAjWj2Uu5QLw6B5TRbA+z2RrYor05AJCdlQ5g88Y/P5bBmE\r\nYIPx2hwWbuIWzeDeRBjejAVGGOGgZYLCnbTze5cCgYBKNDLrkRIDxU9UQu5IhgcD\r\nrbBHLyjvSHc6ugFgUX6VZSh0qwzpNuHHcMSqnQ+RYW5BjqnokhekzPNIR8ucHXGt\r\nRQjP85xGVo8l224kwaYJIYSb37ucJegnzhd6CHcQg4ed93YnzFunnyBBnnKFrOIh\r\nowaUjGGY5YizSz9X403Sxg==\r\n-----END PRIVATE KEY-----";
                    PFXPassword_LinkBank = RepNCHL.pfxPasswordLIVE_LinkBank;
                }
                else
                {
                    //privateFile_NCHLQR = RepNCHL.privateFile_NCHLQR;
                    privateFile_NCHLQR = HttpContext.Current.Server.MapPath(RepNCHL.privateFile_NCHLQR);
                    PFXPassword_LinkBank = RepNCHL.PFXPassword_LinkBank;
                }
                var success = privateKey.LoadEncryptedPem(privateFile_NCHLQR, PFXPassword_LinkBank);
                success = rsa1.ImportPrivateKeyObj(privateKey);
                rsa1.OaepPadding = true;
                rsa1.EncodingMode = "base64";
                rsa1.OaepHash = "sha256";
                originalStr = rsa1.DecryptStringENC(encryptedStr, true);

            }
            catch (Exception ex)
            {

            }
            return originalStr;
        }
        public string Encrypt(string decrypdata)
        {

            Chilkat.Rsa rsa1 = new Chilkat.Rsa();
            Chilkat.PublicKey publicKey1 = new Chilkat.PublicKey();
            string publicFile_NCHLQR = string.Empty;
            if (Common.ApplicationEnvironment.IsProduction == true)
            {
                publicFile_NCHLQR = RepNCHL.publicKeyLIVE_NCHLQR;
            }
            else
            {
                publicFile_NCHLQR = RepNCHL.publicFile_NCHLQR;
            }


            var success = publicKey1.LoadFromFile(HttpContext.Current.Server.MapPath(publicFile_NCHLQR));
            success = rsa1.ImportPublicKeyObj(publicKey1);
            rsa1.OaepPadding = true;
            rsa1.OaepHash = "sha256"; // You can set this to "SHA-256", "SHA-1", or other supported hashing algorithms.
            rsa1.EncodingMode = "base64";
            string originalStr = rsa1.EncryptStringENC(decrypdata, false);
            return originalStr;
        }
        public static string GenerateConnectIPSTokenPrivateKey(string stringToHash)
        {
            try
            {
                string PFXFile_LinkBank = string.Empty;
                string PFXPassword_LinkBank = string.Empty;
                if (Common.ApplicationEnvironment.IsProduction == true)
                {
                    PFXFile_LinkBank = RepNCHL.pfxLIVE_NCHLQR;
                    PFXPassword_LinkBank = RepNCHL.pfxPasswordLIVE_LinkBank;
                }
                else
                {
                    PFXFile_LinkBank = RepNCHL.PFXFile_LinkBank;
                    PFXPassword_LinkBank = RepNCHL.PFXPassword_LinkBank;
                }
                X509Certificate2 privateCert = new X509Certificate2(HttpContext.Current.Server.MapPath(PFXFile_LinkBank), PFXPassword_LinkBank, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
                RSACryptoServiceProvider privateKey = (RSACryptoServiceProvider)privateCert.PrivateKey;

                RSACryptoServiceProvider privateKey1 = new RSACryptoServiceProvider();
                privateKey1.ImportParameters(privateKey.ExportParameters(true));
                byte[] data = Encoding.UTF8.GetBytes(stringToHash);
                byte[] signature = privateKey1.SignData(data, "SHA256");
                string signaturresult = Convert.ToBase64String(signature);
                return signaturresult;

            }
            catch (Exception ex)
            {
                Common.AddLogs(ex.Message, false, 1, Common.CreatedBy, Common.CreatedByName);
                return string.Empty;
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


        /// <summary>
        /// Import OpenSSH PEM public key string into MS RSACryptoServiceProvider
        /// </summary>
        /// <param name="pem"></param>
        /// <returns></returns>


        public static T GetSessionValue<T>(string SessionKey)
        {
            HttpContext context = HttpContext.Current;
            T t = default(T);
            if (context != null)
            {
                var Session = context.Session;
                if (Session != null)
                {
                    if (Session[SessionKey] != null)
                    {
                        t = (T)Session[SessionKey];
                    }
                }
            }
            return t;
        }



    }
}