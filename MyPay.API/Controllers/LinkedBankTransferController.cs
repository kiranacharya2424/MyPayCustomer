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
    public class LinkedBankTransferController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(LinkedBankTransferController));
        public HttpResponseMessage LinkedBankTransfer(Req_DepositByBankTransfer user)
        {

            CommonResponse cres = new CommonResponse();
            var response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);

            var userInput = getRawPostData().Result;

            //Added to disable "Pay using Bank account for Voting and events"
            if ((user.VendorType == "118" || user.VendorType == "98") && !string.IsNullOrEmpty(user.CouponCode)) {
                string results = "Un-Authorized Request";
                cres = CommonApiMethod.ReturnBadRequestMessage(results);
                cres.Message = "You cannot pay using bank while using a promo code. Kindly use wallet to complete the transaction.";
                response.StatusCode = HttpStatusCode.BadRequest;
                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                return response;
            }


            log.Info($"{System.DateTime.Now.ToString()} inside LinkedBankTransfer" + Environment.NewLine);
            
            try
            {
                //cres = CommonApiMethod.ReturnBadRequestMessage("Temporarily service Unavailable. Please try again later!");
                //response.StatusCode = HttpStatusCode.BadRequest;
                //response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                //return response;
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

                        //string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString().Trim() != "0")
                        {
                            int VendorAPIType = 0;
                            int Type = 0;
                            Int64 memId = Convert.ToInt64(user.MemberId);
                            if (user.Type == 1)
                            {
                                VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.Deposit_By_Linked_Bank;
                                Type = (int)VendorApi_CommonHelper.VendorTypes.NPS;
                            }
                            else
                            {
                                VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.Credit_By_Linked_Bank;
                                Type = (int)VendorApi_CommonHelper.VendorTypes.khalti;
                            }
                            AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, user.Amount.ToString(), true, user.Mpin);
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
                        if (user.Type == 2 && user.VendorType == ((int)VendorApi_CommonHelper.KhaltiAPIName.Credit_Card_Payment).ToString())
                        {

                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage("Credit Card Payment with Link Bank is Unavailable. Please try again later!");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        string Ipaddress = Common.GetUserIP();
                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;


                        string PlatForm = user.PlatForm;
                        string DeviceCode = System.Web.HttpContext.Current.Request.Browser.Type;
                        string Reference = (new CommonHelpers()).GenerateUniqueId();
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                        int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.Credit_By_Linked_Bank;
                        if (user.Type == 1)
                        {
                            VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.Deposit_By_Linked_Bank;
                        }
                        string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorApiType).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                        objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse("", Reference, resGetRecord.MemberId, resGetRecord.FirstName, "", "", userInput, DeviceCode, PlatForm, VendorApiType, "", "", "", ((int)VendorApi_CommonHelper.VendorTypes.MyPay).ToString());
                        string ApiResponse = string.Empty;
                        string ReturnTransactionId = string.Empty;
                        string APINAME = "";
                        string msg = RepTransactions.BankTransferByLinkedAccount(resGetRecord, user.VendorType, user.Mpin, authenticationToken, user.Type, user.BankId, user.Description, user.Amount, user.MemberId, user.PlatForm, user.DeviceCode, ref ReturnTransactionId, ref objVendor_API_Requests, ref APINAME);
                        if (msg.ToLower() != "success")
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                        }
                        else
                        {
                            Res_LinkedBankResponse cres1 = new Res_LinkedBankResponse();
                            cres1.Message = "Success";
                            if (user.Type == 1)
                            {
                                cres1.Details = "Payment Successfully Deposit";
                            }
                            else
                            {
                                cres1.Details = "Payment Successfully Debited";
                            }
                            cres1.TransactionId = ReturnTransactionId;
                            cres1.ReponseCode = 1;
                            cres1.status = true;
                            response = Request.CreateResponse<Res_LinkedBankResponse>(System.Net.HttpStatusCode.Created, cres1);
                            log.Info($"{System.DateTime.Now.ToString()} LinkedBankTransfer completed" + Environment.NewLine);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres1);
                        }

                        objVendor_API_Requests.Res_Output = ApiResponse;
                        RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "vendor_api_requests");

                        return response;
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
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} LinkedBankTransfer {ex.ToString()} " + Environment.NewLine);
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