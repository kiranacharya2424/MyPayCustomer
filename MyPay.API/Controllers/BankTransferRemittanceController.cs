using log4net;
using MyPay.API.Models;
using MyPay.API.Models.MyPayPayments;
using MyPay.API.Models.Request;
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
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace MyPay.API.Controllers
{
    public class BankTransferRemittanceController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(BankTransferRemittanceController));

        [System.Web.Http.HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [System.Web.Http.Route("api/bank-account-validation")]
        public HttpResponseMessage BankAccountValidation(Req_BankTransfer_Remittance user)
        {
            log.Info($" {System.DateTime.Now.ToString()} inside AccountValidation Started" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            var response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
            try
            {
                if (HttpContext.Current.Request.Headers.GetValues("API_KEY") == null)
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("API_KEY Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }
                else if (HttpContext.Current.Request.Headers.GetValues("Signature") == null)
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("Signature Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.AuthTokenString))
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("AuthTokenString Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.Reference))
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("Reference Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }

                else if (string.IsNullOrEmpty(user.BankCode))
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage("Please Enter Bank Code");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.AccountName))
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage("Please Enter Bank Account Name");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.AccountNumber))
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage("Please Enter Bank Account Number");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.Description))
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage("Please Enter Description");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else if (user.Amount == null || user.Amount == 0)
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage("Please Enter Amount Greater Than 0");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else
                {
                    string UserInput = getRawPostData().Result;
                    string ApiResponse = string.Empty;
                    AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                    Req_AccountValidation req_AccountValidation = new Req_AccountValidation();
                    //string authenticationToken = Request.Headers.Authorization.Parameter;
                    string authenticationToken = string.Empty;
                    Common.authenticationToken = authenticationToken;

                    string API_KEY = HttpContext.Current.Request.Headers.GetValues("API_KEY")[0].ToString();
                    string Signature = HttpContext.Current.Request.Headers.GetValues("Signature")[0].ToString();
                    string UniqueTransactionId = string.Empty;
                    string SenderTransactionId = string.Empty;
                    string RedirectURL = string.Empty;
                    string OrderToken = string.Empty;
                    string msg = string.Empty;
                    string PlatForm = "Web";

                    string DeviceCode = HttpContext.Current.Request.UserAgent.ToLower();
                    bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                    string Reference = Common.GenerateReferenceUniqueID();
                    int VendorApiType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.bank_transfer;
                    string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorApiType).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                    objVendor_API_Requests = VendorApi_CommonHelper.SendDataToRemittance_SaveResponse("", Reference, Common.CreatedBy, Common.CreatedByName, "", authenticationToken, UserInput, user.DeviceCode, user.PlatForm, VendorApiType);

                    msg = RepMerchants.CheckMerchantRemittanceCredentials(API_KEY, user.MerchantId, user.Amount.ToString(), user.UserName, user.Password, VendorApiType, PlatForm, DeviceCode, user.Description, Reference, user.AuthTokenString, UserInput, Signature, ref UniqueTransactionId, ref SenderTransactionId, ref RedirectURL, ref OrderToken, ref objVendor_API_Requests);
                    if (msg.ToLower() == "success")
                    {
                        string BankType = "NCHL";
                        req_AccountValidation.bankId = user.BankCode;
                        req_AccountValidation.accountId = user.AccountNumber;
                        req_AccountValidation.accountName = user.AccountName;
                        using (MyPayEntities db = new MyPayEntities())
                        {
                            ApiSetting objApiSettings = db.ApiSettings.FirstOrDefault();
                            if (objApiSettings != null && objApiSettings.BankTransferType > 0)
                            {
                                if (objApiSettings.BankTransferType == 2)
                                {
                                    BankType = "NPS";
                                }
                                else
                                {
                                    BankType = "NCHL";
                                }
                            }

                        }
                        if (BankType != "NPS")
                        {
                            string token = RepNCHL.gettoken();
                            string ApiName = "api/validatebankaccount";
                            string data = RepNCHL.PostMethodWithToken(ApiName, JsonConvert.SerializeObject(req_AccountValidation), token);
                            objVendor_API_Requests.Req_Khalti_URL = ApiName;
                            Common.AddLogs($"URL: {ApiName} Request: {JsonConvert.SerializeObject(user)} Response:{data} ", false, (int)AddLog.LogType.DBLogs);
                            if (!string.IsNullOrEmpty(data))
                            {
                                Res_AccountValidation res = JsonConvert.DeserializeObject<Res_AccountValidation>(data);
                                if (res.responseCode == "000")
                                {
                                    //List<GetNCBranchList> list = RepNCHL.GetBranchList("cips", "");
                                    //GetNCBranchList objList = list.Where(c => c.branchId == res.branchId).FirstOrDefault();
                                    //res.branchName = objList.branchName;
                                    res.ReponseCode = 1;
                                    res.Message = "Success";
                                    res.Details = res.responseMessage;
                                    res.status = true;
                                    res.MatchPercentate = res.MatchPercentate;
                                    response.StatusCode = HttpStatusCode.Created;
                                    response = Request.CreateResponse<Res_AccountValidation>(System.Net.HttpStatusCode.Created, res);
                                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(res);

                                    objVendor_API_Requests.Res_Output = ApiResponse;
                                    RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "remittance_api_requests");
                                    return response;
                                }
                                else if (res.responseCode == "999")
                                {
                                    res.ReponseCode = 2;
                                    res.Message = "Success";
                                    res.Details = res.responseMessage;
                                    res.status = true;
                                    res.MatchPercentate = res.MatchPercentate;
                                    response.StatusCode = HttpStatusCode.Created;
                                    response = Request.CreateResponse<Res_AccountValidation>(System.Net.HttpStatusCode.Created, res);
                                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(res);

                                    objVendor_API_Requests.Res_Output = ApiResponse;
                                    RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "remittance_api_requests");
                                    return response;
                                }
                                else
                                {
                                    res.ReponseCode = 3;
                                    res.Details = res.responseMessage;
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    response = Request.CreateResponse<Res_AccountValidation>(System.Net.HttpStatusCode.BadRequest, res);
                                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(res);
                                    res.MatchPercentate = res.MatchPercentate;
                                    objVendor_API_Requests.Res_Output = ApiResponse;
                                    RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "remittance_api_requests");
                                    return response;
                                }
                            }
                            else
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage("Data Not Foud");
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);

                                objVendor_API_Requests.Res_Output = ApiResponse;
                                RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "remittance_api_requests");
                                return response;
                            }
                        }
                        else
                        {
                            objVendor_API_Requests.Req_Khalti_URL = "v1/AccountValidation";
                            GetFundAccountValidation valid = RepNps.FundAccountValidation(user.AccountName, user.AccountNumber, user.BankCode);
                            if (valid.code == "0" || (valid.data != null && Convert.ToInt32(valid.data.NameMatchPercentage) >= 80))
                            {
                                Res_AccountValidation cres1 = new Res_AccountValidation();
                                cres1.ReponseCode = 1;
                                cres1.Message = "Success";
                                cres1.Details = valid.message;
                                cres1.status = true;
                                response.StatusCode = HttpStatusCode.Created;
                                response = Request.CreateResponse<Res_AccountValidation>(System.Net.HttpStatusCode.Created, cres1);
                                ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres1);

                                objVendor_API_Requests.Res_Output = ApiResponse;
                                RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "remittance_api_requests");
                                return response;
                            }
                            else
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage(valid.message);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                                objVendor_API_Requests.Res_Output = ApiResponse;
                                RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "remittance_api_requests");
                                return response;
                            }
                        }
                    }
                    else
                    {
                        cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
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
                log.Error($"====== {System.DateTime.Now.ToString()}  AccountValidation {ex.ToString()} ===");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [System.Web.Http.HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [System.Web.Http.Route("api/bank-transfer-remittance")]
        public HttpResponseMessage BankTransferRemittance(Req_BankTransfer_Remittance user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside BankTransfer" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            var response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
            try
            {
                if (HttpContext.Current.Request.Headers.GetValues("API_KEY") == null)
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("API_KEY Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }
                else if (HttpContext.Current.Request.Headers.GetValues("Signature") == null)
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("Signature Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.AuthTokenString))
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("AuthTokenString Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.Reference))
                {
                    CommonResponse cres1 = new CommonResponse();
                    cres1 = CommonApiMethod.ReturnBadRequestMessage("Reference Not Found");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                    return response;
                }

                else if (string.IsNullOrEmpty(user.BankCode))
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage("Please Enter Bank Code");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                //else if (string.IsNullOrEmpty(user.BranchId))
                //{
                //    cres = CommonApiMethod.ReturnBadRequestMessage("Please Enter Branch Name");
                //    response.StatusCode = HttpStatusCode.BadRequest;
                //    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                //    return response;
                //}
                else if (string.IsNullOrEmpty(user.AccountName))
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage("Please Enter Bank Account Name");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.AccountNumber))
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage("Please Enter Bank Account Number");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.Description))
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage("Please Enter Description");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else if (user.Amount == null || user.Amount == 0)
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage("Please Enter Amount Greater Than 0");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else
                {
                    AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                    AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();

                    string UserInput = getRawPostData().Result;
                    string API_KEY = HttpContext.Current.Request.Headers.GetValues("API_KEY")[0].ToString();
                    string Signature = HttpContext.Current.Request.Headers.GetValues("Signature")[0].ToString();
                    string UniqueTransactionId = string.Empty;
                    string SenderTransactionId = string.Empty;
                    string RedirectURL = string.Empty;
                    string OrderToken = string.Empty;
                    AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                    string msg = string.Empty;
                    string PlatForm = "Web";
                    string DeviceCode = HttpContext.Current.Request.UserAgent.ToLower();

                    string Reference = user.Reference;
                    //int VendorApiType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.Merchant_Bank_Load;
                    int VendorApiType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.bank_transfer;
                    string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorApiType).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                    //objVendor_API_Requests = VendorApi_CommonHelper.SendDataToRemittance_SaveResponse("", Reference, Common.CreatedBy, Common.CreatedByName, "", "", UserInput, DeviceCode, PlatForm, VendorApiType);

                    //// *** SAVE API REQUEST
                    //#region SaveAPIRequest
                    string ApiResponse = string.Empty;
                    bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                    VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.bank_transfer;
                    AddVendor_API_Requests resVendor_API_Requests = new AddVendor_API_Requests();
                    string instructionId = DateTime.UtcNow.ToString("ddMMyyyyhhmmss") + Common.RandomNumber(1111, 9999);
                    string Req_ReferenceNo = instructionId;
                    string JsonReq = string.Empty;
                    //string authenticationToken = Request.Headers.Authorization.Parameter;
                    string authenticationToken = string.Empty;
                    Common.authenticationToken = authenticationToken;
                    //resVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse("", Req_ReferenceNo, resuserdetails.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, JsonReq, authenticationToken, UserInput, user.DeviceCode, user.PlatForm, VendorApiType);
                    //resVendor_API_Requests = VendorApi_CommonHelper.SendDataToRemittance_SaveResponse("", Req_ReferenceNo, resuserdetails.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, JsonReq, authenticationToken, UserInput, user.DeviceCode, user.PlatForm, VendorApiType);
                    resVendor_API_Requests = VendorApi_CommonHelper.SendDataToRemittance_SaveResponse("", Reference, Common.CreatedBy, Common.CreatedByName, "", "", UserInput, DeviceCode, PlatForm, VendorApiType);

                    //#endregion

                    //msg = RepMerchants.RequestMerchantWalletTransaction(API_KEY, user.ContactNumber, user.MerchantId, user.Amount, user.UserName, user.Password, VendorApiType, PlatForm, DeviceCode, user.Remarks, Reference, user.AuthTokenString, UserInput, Signature, ref UniqueTransactionId, ref SenderTransactionId, ref RedirectURL, ref OrderToken, ref objVendor_API_Requests);
                    //AddCalculateServiceChargeAndCashback objOut = MyPay.Models.Common.Common.CalculateNetAmountWithServiceCharge(user.MemberId.ToString(), user.Amount.ToString(), ((int)VendorApi_CommonHelper.KhaltiAPIName.bank_transfer).ToString());


                    msg = RepMerchants.CheckMerchantRemittanceCredentials(API_KEY, user.MerchantId, user.Amount.ToString(), user.UserName, user.Password, VendorApiType, PlatForm, DeviceCode, user.Description, Reference, user.AuthTokenString, UserInput, Signature, ref UniqueTransactionId, ref SenderTransactionId, ref RedirectURL, ref OrderToken, ref objVendor_API_Requests);
                    if (msg.ToLower() == "success")
                    {
                        AddNCBankTransfer bank = new AddNCBankTransfer();
                        bank.cipsBatchDetail.batchId = DateTime.UtcNow.ToString("ddMMyyyyhhmmss") + Common.RandomNumber(1111, 9999);
                        bank.cipsBatchDetail.batchAmount = Convert.ToDecimal(user.Amount).ToString("f2");
                        bank.cipsBatchDetail.batchCount = 1;
                        bank.cipsBatchDetail.batchCrncy = "NPR";
                        bank.cipsBatchDetail.categoryPurpose = "ECPG";

                        if (!Common.ApplicationEnvironment.IsProduction)
                        {
                            bank.cipsBatchDetail.debtorAgent = Common.ConnectIps_BankId_Staging;
                            bank.cipsBatchDetail.debtorBranch = Common.ConnectIPs_BranchId_Staging;
                            bank.cipsBatchDetail.debtorName = Common.ConnectIps_AccountName_Staging;
                            bank.cipsBatchDetail.debtorAccount = Common.ConnectIPs_AccountNumber_Staging;
                        }
                        else
                        {
                            bank.cipsBatchDetail.debtorAgent = Common.ConnectIps_BankId;
                            bank.cipsBatchDetail.debtorBranch = Common.ConnectIPs_BranchId;
                            bank.cipsBatchDetail.debtorName = Common.ConnectIps_AccountName;
                            bank.cipsBatchDetail.debtorAccount = Common.ConnectIPs_AccountNumber;
                        }

                        string batch = bank.cipsBatchDetail.batchId + "," + bank.cipsBatchDetail.debtorAgent + "," + bank.cipsBatchDetail.debtorBranch + "," + bank.cipsBatchDetail.debtorAccount + "," + bank.cipsBatchDetail.batchAmount + "," + bank.cipsBatchDetail.batchCrncy;

                        AddNCTransactionDetail transactionlist = new AddNCTransactionDetail();

                        transactionlist.instructionId = instructionId;
                        transactionlist.endToEndId = Common.TruncateLongString(user.Description, 29);
                        transactionlist.amount = Convert.ToDecimal(user.Amount).ToString("f2");
                        transactionlist.creditorAgent = user.BankCode;
                        transactionlist.creditorBranch = "1"; // user.BranchId;
                        transactionlist.creditorName = user.AccountName;
                        transactionlist.creditorAccount = user.AccountNumber;
                        //transactionlist.freeCode1 = user.MemberId.ToString();
                        transactionlist.freeCode1 = user.MerchantId;
                        bank.cipsTransactionDetailList.Add(transactionlist);
                        string transaction = bank.cipsTransactionDetailList[0].instructionId + "," + bank.cipsTransactionDetailList[0].creditorAgent + "," + bank.cipsTransactionDetailList[0].creditorBranch + "," + bank.cipsTransactionDetailList[0].creditorAccount + "," + bank.cipsTransactionDetailList[0].amount;


                        if (!Common.ApplicationEnvironment.IsProduction)
                        {
                            bank.token = Common.GenerateConnectIPSToken2(batch + "," + transaction + "," + RepNCHL.withdrawusername_staging);
                        }
                        else
                        {
                            bank.token = Common.GenerateConnectIPSToken2(batch + "," + transaction + "," + RepNCHL.withdrawusername);
                        }


                        string BankType = "NCHL";
                        using (MyPayEntities db = new MyPayEntities())
                        {
                            ApiSetting objApiSettings = db.ApiSettings.FirstOrDefault();
                            if (objApiSettings != null && objApiSettings.BankTransferType > 0)
                            {
                                if (objApiSettings.BankTransferType == 2)
                                {
                                    BankType = "NPS";
                                }
                                else
                                {
                                    BankType = "NCHL";
                                }
                            }

                        }
                        string token = "";
                        if (BankType != "NPS")
                        {
                            token = RepNCHL.gettoken();
                        }

                        AddDepositOrders outobjectver = new AddDepositOrders();
                        outobjectver.Amount = Convert.ToDecimal(user.Amount);
                        //outobjectver.CreatedBy = Common.GetCreatedById(authenticationToken);
                        //outobjectver.CreatedByName = Common.GetCreatedByName(authenticationToken);
                        outobjectver.CreatedBy = Common.CreatedBy;
                        outobjectver.CreatedByName = Common.CreatedByName;
                        outobjectver.TransactionId = bank.cipsBatchDetail.batchId;
                        outobjectver.RefferalsId = bank.cipsTransactionDetailList[0].instructionId;
                        outobjectver.MemberId = Convert.ToInt64(resuserdetails.MemberId);
                        outobjectver.Type = (int)AddDepositOrders.DepositType.Bank_Transfer;
                        outobjectver.Remarks = Common.TruncateLongString(user.Description, 29);
                        outobjectver.Particulars = "Bank Transfer Initiate";
                        outobjectver.Status = (int)AddDepositOrders.DepositStatus.Incomplete;
                        outobjectver.IsActive = true;
                        outobjectver.IsApprovedByAdmin = true;
                        outobjectver.ServiceCharges = 0;
                        //outobjectver.ServiceCharges = objOut.ServiceCharge;
                        outobjectver.Platform = user.PlatForm;
                        outobjectver.DeviceCode = user.DeviceCode;

                        if (BankType != "NPS")
                        {
                            outobjectver.GatewayType = (int)VendorApi_CommonHelper.VendorTypes.NCHL;
                            outobjectver.GatewayName = VendorApi_CommonHelper.VendorTypes.NCHL.ToString();
                        }
                        else
                        {
                            outobjectver.GatewayType = (int)VendorApi_CommonHelper.VendorTypes.NPS;
                            outobjectver.GatewayName = VendorApi_CommonHelper.VendorTypes.NPS.ToString();
                        }
                        Int64 Id = RepCRUD<AddDepositOrders, GetDepositOrders>.Insert(outobjectver, "depositorders");

                        if (Id > 0)
                        {
                            //string transactionresult = WalletDeduct(outobjectver, resuserdetails, bank, authenticationToken, Common.TruncateLongString(user.Description, 29), user.BankName, user.BranchName, resVendor_API_Requests, user.PlatForm, user.DeviceCode, objOut, BankType);
                            AddMerchant model = new AddMerchant();

                            AddMerchant outobjectmer = new AddMerchant();
                            GetMerchant inobjectmer = new GetMerchant();
                            //inobjectmer.MerchantUniqueId = MerchantUniqueId;
                            inobjectmer.MerchantUniqueId = user.MerchantId;
                            model = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobjectmer, outobjectmer);

                            string UserMessage = string.Empty;
                            string TransactionAmount = user.Amount.ToString();
                            string Referenceno = new CommonHelpers().GenerateUniqueId();
                            string AdminRemarksCD = "Remittance Transaction Request to Bank.";
                            string TransactionType = "2";

                            //for debit from merchat balance (deduct from merchant balance)
                            string transactionresult = RepTransactions.MerchantWalletUpdateFromAdmin(model, TransactionAmount, Referenceno, TransactionType, ref UserMessage, AdminRemarksCD, "");

                            if (transactionresult == "success")
                            {

                                string data = "";
                                string APINAME = "";
                                GetFundTransferRequest fundres = new GetFundTransferRequest();
                                if (BankType != "NPS")
                                {
                                    APINAME = "api/postcipsbatch";
                                    data = RepNCHL.PostMethodWithToken(APINAME, JsonConvert.SerializeObject(bank), token);
                                    Common.AddLogs($"BankTransfer URL {APINAME} Request: {JsonConvert.SerializeObject(bank)} Response: {data}", false, (int)AddLog.LogType.DBLogs);
                                }
                                else
                                {
                                    APINAME = "v1/FundTransferRequest";
                                    fundres = RepNps.FundTransferRequest(user.AccountName, user.AccountNumber, user.BankId, bank.cipsBatchDetail.batchId, bank.cipsTransactionDetailList[0].instructionId, user.Amount.ToString(), user.Description);
                                }

                                if (!string.IsNullOrEmpty(data) || !string.IsNullOrEmpty(fundres.message))
                                {

                                    AddDepositOrders outobject = new AddDepositOrders();
                                    GetDepositOrders inobject = new GetDepositOrders();
                                    inobject.Id = Id;
                                    AddDepositOrders resDeposit = RepCRUD<GetDepositOrders, AddDepositOrders>.GetRecord(nameof(Common.StoreProcedures.sp_DepositOrders_Get), inobject, outobject);
                                    if (resDeposit.Id > 0)
                                    {
                                        if (data.Contains("fieldErrors") || fundres.errors.Count > 0)
                                        {
                                            GetBankTransferErrors res1 = new GetBankTransferErrors();
                                            if (BankType != "NPS")
                                            {
                                                res1 = JsonConvert.DeserializeObject<GetBankTransferErrors>(data);
                                            }
                                            if (res1.fieldErrors != null && res1.fieldErrors.Count > 0)
                                            {
                                                resDeposit.Particulars = res1.fieldErrors[0].message;
                                            }
                                            else if (fundres.errors != null && fundres.errors.Count > 0)
                                            {
                                                resDeposit.Particulars = fundres.errors[0].error_message;
                                            }
                                            else
                                            {
                                                resDeposit.Particulars = res1.responseDescription;

                                            }
                                            resDeposit.Status = (int)AddDepositOrders.DepositStatus.Failed;

                                            if (BankType != "NPS")
                                            {
                                                resDeposit.JsonResponse = data;
                                                resDeposit.ResponseCode = res1.responseCode;
                                            }
                                            else
                                            {
                                                resDeposit.JsonResponse = JsonConvert.SerializeObject(fundres);
                                                resDeposit.ResponseCode = fundres.code;
                                            }
                                            RepCRUD<AddDepositOrders, GetDepositOrders>.Update(resDeposit, "depositorders");

                                            cres = CommonApiMethod.ReturnBadRequestMessage(resDeposit.Particulars);

                                            AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                                            GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                                            inobjApiResponse.Id = resVendor_API_Requests.Id;
                                            AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_RemittanceAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                                            if (resUpdateRecord != null && resUpdateRecord.Id != 0)
                                            {
                                                resUpdateRecord.Req_Khalti_Input = JsonConvert.SerializeObject(bank);
                                                resUpdateRecord.Res_Khalti_Output = resDeposit.JsonResponse;
                                                resUpdateRecord.Res_Khalti_Message = resDeposit.Particulars;
                                                resUpdateRecord.Res_Khalti_Id = resDeposit.RefferalsId;
                                                resUpdateRecord.Res_Khalti_State = AddDepositOrders.DepositStatus.Failed.ToString();
                                                resUpdateRecord.Res_Khalti_Status = false;
                                                resUpdateRecord.Res_Output = JsonConvert.SerializeObject(cres);
                                                resUpdateRecord.Req_Khalti_URL = APINAME;
                                                RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "remittance_api_requests");
                                            }
                                            Common.AddLogs(resDeposit.Particulars, false, Convert.ToInt32(AddLog.LogType.BankTransfer), resuserdetails.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, true, user.PlatForm, user.DeviceCode);
                                            //string msgresult = WalletRefund(instructionId, resuserdetails, authenticationToken, user.PlatForm, user.DeviceCode);

                                            AdminRemarksCD = "Remittance Refund Transaction.";
                                            TransactionType = "1";
                                            //for credit from merchat balance (refund)
                                            string refundtransactionresult = RepTransactions.MerchantWalletUpdateFromAdmin(model, TransactionAmount, Referenceno, TransactionType, ref UserMessage, AdminRemarksCD, "");

                                            response.StatusCode = HttpStatusCode.BadRequest;
                                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                            return response;
                                        }
                                        GetBankTransfer res = new GetBankTransfer();
                                        if (BankType != "NPS")
                                        {
                                            res = JsonConvert.DeserializeObject<GetBankTransfer>(data);
                                        }

                                        if ((fundres.code == "0") || (fundres.code == "2") || ((res.cipsBatchResponse.responseCode == "000" && res.cipsBatchResponse.responseMessage == "SUCCESS") && (res.cipsTxnResponseList[0].responseCode == "000" || res.cipsTxnResponseList[0].responseCode == "999" || res.cipsTxnResponseList[0].responseCode == "DEFER") && res.cipsTxnResponseList[0].responseMessage.ToLower() != "invalid account number"))
                                        {
                                            WalletTransactions restra = new WalletTransactions();
                                            restra.TransactionUniqueId = instructionId;

                                            if (fundres.code == "2" || (res.cipsTxnResponseList.Count > 0 && (res.cipsTxnResponseList[0].responseCode == "DEFER" || res.cipsTxnResponseList[0].responseCode == "999")))
                                            {
                                                restra.Status = (int)WalletTransactions.Statuses.Pending;
                                                restra.GatewayStatus = WalletTransactions.Statuses.Pending.ToString();
                                                if (BankType != "NPS")
                                                {
                                                    restra.ResponseCode = res.cipsTxnResponseList[0].responseCode;
                                                }
                                                else
                                                {
                                                    restra.ResponseCode = fundres.code;
                                                }

                                                resDeposit.Status = (int)AddDepositOrders.DepositStatus.Pending;
                                            }
                                            else
                                            {
                                                restra.Status = (int)WalletTransactions.Statuses.Success;

                                                if (BankType != "NPS")
                                                {
                                                    restra.GatewayStatus = res.cipsBatchResponse.responseMessage;
                                                    restra.ResponseCode = res.cipsTxnResponseList[0].responseCode;
                                                }
                                                else
                                                {
                                                    restra.GatewayStatus = fundres.data.TransactionStatus;
                                                    restra.ResponseCode = fundres.code;
                                                }

                                                resDeposit.Status = (int)AddDepositOrders.DepositStatus.Success;
                                            }
                                            if (BankType != "NPS")
                                            {
                                                restra.VendorTransactionId = res.cipsTxnResponseList[0].id;
                                                //restra.Reference = res.cipsBatchResponse.id;
                                                restra.BatchTransactionId = res.cipsBatchResponse.id;
                                                restra.TxnInstructionId = res.cipsTxnResponseList[0].id;
                                                restra.ResponseCode = res.cipsBatchResponse.responseCode;
                                            }
                                            else
                                            {
                                                restra.VendorTransactionId = fundres.data.TransactionId;
                                                //restra.Reference = res.cipsBatchResponse.id;
                                                restra.BatchTransactionId = fundres.data.MerchantTxnId;
                                                restra.TxnInstructionId = fundres.data.MerchantTxnId;
                                                restra.ResponseCode = fundres.code;
                                            }

                                            if (BankType != "NPS")
                                            {
                                                resDeposit.RefferalsId = res.cipsTxnResponseList[0].id;
                                                resDeposit.Particulars = res.cipsTxnResponseList[0].responseMessage;
                                                resDeposit.JsonResponse = data;
                                                resDeposit.ResponseCode = res.cipsTxnResponseList[0].responseCode;
                                            }
                                            else
                                            {
                                                resDeposit.RefferalsId = fundres.data.TransactionId;
                                                resDeposit.Particulars = fundres.message;
                                                resDeposit.JsonResponse = JsonConvert.SerializeObject(fundres);
                                                resDeposit.ResponseCode = fundres.code;
                                            }
                                            RepCRUD<AddDepositOrders, GetDepositOrders>.Update(resDeposit, "depositorders");

                                            AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                                            GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                                            inobjApiResponse.Id = resVendor_API_Requests.Id;
                                            AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_RemittanceAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                                            if (resUpdateRecord != null && resUpdateRecord.Id != 0)
                                            {
                                                resUpdateRecord.Req_Khalti_Input = JsonConvert.SerializeObject(bank);
                                                if (BankType != "NPS")
                                                {
                                                    resUpdateRecord.Res_Khalti_Output = JsonConvert.SerializeObject(res);
                                                    resUpdateRecord.Req_Khalti_URL = APINAME;
                                                }
                                                else
                                                {
                                                    resUpdateRecord.Res_Khalti_Output = JsonConvert.SerializeObject(fundres);
                                                    resUpdateRecord.Req_Khalti_URL = APINAME;
                                                }
                                                resUpdateRecord.Res_Khalti_Message = resDeposit.Particulars;
                                                resUpdateRecord.Res_Khalti_Id = resDeposit.RefferalsId;
                                                resUpdateRecord.Res_Khalti_State = AddDepositOrders.DepositStatus.Success.ToString();
                                                resUpdateRecord.Res_Khalti_Status = true;
                                                resUpdateRecord.Res_Output = JsonConvert.SerializeObject(cres);
                                                RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "remittance_api_requests");
                                            }

                                            #region SendEmailConfirmation
                                            string mystring = File.ReadAllText(HttpContext.Current.Server.MapPath("/Templates/BankTransfer.html"));
                                            string body = mystring;
                                            body = body.Replace("##Amount##", (restra.Amount).ToString("0.00"));
                                            body = body.Replace("##TransactionId##", restra.TransactionUniqueId);
                                            body = body.Replace("##Date##", restra.CreatedDate.ToString("dd-MMM-yyyy hh:mm:ss tt"));
                                            body = body.Replace("##Type##", "Bank Transfer");
                                            body = body.Replace("##Status##", WalletTransactions.Statuses.Success.ToString());
                                            //body = body.Replace("##Cashback##", objOut.CashbackAmount.ToString("0.00"));
                                            //body = body.Replace("##ServiceCharge##", objOut.ServiceCharge.ToString("0.00"));
                                            body = body.Replace("##Cashback##", "0.00");
                                            body = body.Replace("##ServiceCharge##", "0.00");
                                            body = body.Replace("##Purpose##", restra.Description);

                                            string Subject = MyPay.Models.Common.Common.WebsiteName + " - Bank Transfer Successfull";
                                            if (!string.IsNullOrEmpty(resuserdetails.Email))
                                            {
                                                body = body.Replace("##UserName##", resuserdetails.FirstName);
                                                Common.SendAsyncMail(resuserdetails.Email, Subject, body);
                                            }

                                            #endregion
                                            cres.responseMessage = "Payment Successfully Sent";
                                            cres.Message = "Success";
                                            cres.Details = "Payment Successfully Sent";
                                            cres.ReponseCode = 1;
                                            cres.status = true;
                                            cres.TransactionUniqueId = instructionId;
                                            response.StatusCode = HttpStatusCode.Created;
                                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.Created, cres);
                                            log.Info($"{System.DateTime.Now.ToString()} BankTransfer completed" + Environment.NewLine);
                                            return response;


























                                            //if (restra.GetRecord())
                                            //{
                                            //    if (fundres.code == "2" || (res.cipsTxnResponseList.Count > 0 && (res.cipsTxnResponseList[0].responseCode == "DEFER" || res.cipsTxnResponseList[0].responseCode == "999")))
                                            //    {
                                            //        restra.Status = (int)WalletTransactions.Statuses.Pending;
                                            //        restra.GatewayStatus = WalletTransactions.Statuses.Pending.ToString();
                                            //        if (BankType != "NPS")
                                            //        {
                                            //            restra.ResponseCode = res.cipsTxnResponseList[0].responseCode;
                                            //        }
                                            //        else
                                            //        {
                                            //            restra.ResponseCode = fundres.code;
                                            //        }

                                            //        resDeposit.Status = (int)AddDepositOrders.DepositStatus.Pending;
                                            //    }
                                            //    else
                                            //    {
                                            //        restra.Status = (int)WalletTransactions.Statuses.Success;

                                            //        if (BankType != "NPS")
                                            //        {
                                            //            restra.GatewayStatus = res.cipsBatchResponse.responseMessage;
                                            //            restra.ResponseCode = res.cipsTxnResponseList[0].responseCode;
                                            //        }
                                            //        else
                                            //        {
                                            //            restra.GatewayStatus = fundres.data.TransactionStatus;
                                            //            restra.ResponseCode = fundres.code;
                                            //        }

                                            //        resDeposit.Status = (int)AddDepositOrders.DepositStatus.Success;
                                            //    }
                                            //    if (BankType != "NPS")
                                            //    {
                                            //        restra.VendorTransactionId = res.cipsTxnResponseList[0].id;
                                            //        //restra.Reference = res.cipsBatchResponse.id;
                                            //        restra.BatchTransactionId = res.cipsBatchResponse.id;
                                            //        restra.TxnInstructionId = res.cipsTxnResponseList[0].id;
                                            //        restra.ResponseCode = res.cipsBatchResponse.responseCode;
                                            //    }
                                            //    else
                                            //    {
                                            //        restra.VendorTransactionId = fundres.data.TransactionId;
                                            //        //restra.Reference = res.cipsBatchResponse.id;
                                            //        restra.BatchTransactionId = fundres.data.MerchantTxnId;
                                            //        restra.TxnInstructionId = fundres.data.MerchantTxnId;
                                            //        restra.ResponseCode = fundres.code;
                                            //    }

                                            //    if (restra.Update())
                                            //    {
                                            //        if (BankType != "NPS")
                                            //        {
                                            //            resDeposit.RefferalsId = res.cipsTxnResponseList[0].id;
                                            //            resDeposit.Particulars = res.cipsTxnResponseList[0].responseMessage;
                                            //            resDeposit.JsonResponse = data;
                                            //            resDeposit.ResponseCode = res.cipsTxnResponseList[0].responseCode;
                                            //        }
                                            //        else
                                            //        {
                                            //            resDeposit.RefferalsId = fundres.data.TransactionId;
                                            //            resDeposit.Particulars = fundres.message;
                                            //            resDeposit.JsonResponse = JsonConvert.SerializeObject(fundres);
                                            //            resDeposit.ResponseCode = fundres.code;
                                            //        }
                                            //        RepCRUD<AddDepositOrders, GetDepositOrders>.Update(resDeposit, "depositorders");

                                            //        AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                                            //        GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                                            //        inobjApiResponse.Id = resVendor_API_Requests.Id;
                                            //        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                                            //        if (resUpdateRecord != null && resUpdateRecord.Id != 0)
                                            //        {
                                            //            resUpdateRecord.Req_Khalti_Input = JsonConvert.SerializeObject(bank);
                                            //            if (BankType != "NPS")
                                            //            {
                                            //                resUpdateRecord.Res_Khalti_Output = JsonConvert.SerializeObject(res);
                                            //                resUpdateRecord.Req_Khalti_URL = APINAME;
                                            //            }
                                            //            else
                                            //            {
                                            //                resUpdateRecord.Res_Khalti_Output = JsonConvert.SerializeObject(fundres);
                                            //                resUpdateRecord.Req_Khalti_URL = APINAME;
                                            //            }
                                            //            resUpdateRecord.Res_Khalti_Message = resDeposit.Particulars;
                                            //            resUpdateRecord.Res_Khalti_Id = resDeposit.RefferalsId;
                                            //            resUpdateRecord.Res_Khalti_State = AddDepositOrders.DepositStatus.Success.ToString();
                                            //            resUpdateRecord.Res_Khalti_Status = true;
                                            //            resUpdateRecord.Res_Output = JsonConvert.SerializeObject(cres);
                                            //            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                                            //        }

                                            //        #region SendEmailConfirmation
                                            //        string mystring = File.ReadAllText(HttpContext.Current.Server.MapPath("/Templates/BankTransfer.html"));
                                            //        string body = mystring;
                                            //        body = body.Replace("##Amount##", (restra.Amount).ToString("0.00"));
                                            //        body = body.Replace("##TransactionId##", restra.TransactionUniqueId);
                                            //        body = body.Replace("##Date##", restra.CreatedDate.ToString("dd-MMM-yyyy hh:mm:ss tt"));
                                            //        body = body.Replace("##Type##", "Bank Transfer");
                                            //        body = body.Replace("##Status##", WalletTransactions.Statuses.Success.ToString());
                                            //        //body = body.Replace("##Cashback##", objOut.CashbackAmount.ToString("0.00"));
                                            //        //body = body.Replace("##ServiceCharge##", objOut.ServiceCharge.ToString("0.00"));
                                            //        body = body.Replace("##Cashback##", "0.00");
                                            //        body = body.Replace("##ServiceCharge##", "0.00");
                                            //        body = body.Replace("##Purpose##", restra.Description);

                                            //        string Subject = MyPay.Models.Common.Common.WebsiteName + " - Bank Transfer Successfull";
                                            //        if (!string.IsNullOrEmpty(resuserdetails.Email))
                                            //        {
                                            //            body = body.Replace("##UserName##", resuserdetails.FirstName);
                                            //            Common.SendAsyncMail(resuserdetails.Email, Subject, body);
                                            //        }

                                            //        #endregion
                                            //        cres.responseMessage = "Payment Successfully Sent";
                                            //        cres.Message = "Success";
                                            //        cres.Details = "Payment Successfully Sent";
                                            //        cres.ReponseCode = 1;
                                            //        cres.status = true;
                                            //        response.StatusCode = HttpStatusCode.Created;
                                            //        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.Created, cres);
                                            //        log.Info($"{System.DateTime.Now.ToString()} BankTransfer completed" + Environment.NewLine);
                                            //        return response;

                                            //    }
                                            //    else
                                            //    {
                                            //        resDeposit.Status = (int)AddDepositOrders.DepositStatus.Failed;
                                            //        if (BankType != "NPS")
                                            //        {
                                            //            resDeposit.Particulars = res.cipsTxnResponseList[0].responseMessage;
                                            //            resDeposit.JsonResponse = data;
                                            //            resDeposit.ResponseCode = res.cipsTxnResponseList[0].responseCode;
                                            //        }
                                            //        else
                                            //        {
                                            //            resDeposit.Particulars = fundres.message;
                                            //            resDeposit.JsonResponse = JsonConvert.SerializeObject(fundres);
                                            //            resDeposit.ResponseCode = fundres.code;
                                            //        }
                                            //        RepCRUD<AddDepositOrders, GetDepositOrders>.Update(resDeposit, "depositorders");
                                            //        cres = CommonApiMethod.ReturnBadRequestMessage("Transaction Not Update");
                                            //        AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                                            //        GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                                            //        inobjApiResponse.Id = resVendor_API_Requests.Id;
                                            //        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                                            //        if (resUpdateRecord != null && resUpdateRecord.Id != 0)
                                            //        {
                                            //            resUpdateRecord.Req_Khalti_Input = JsonConvert.SerializeObject(bank);
                                            //            resUpdateRecord.Res_Khalti_Output = data;
                                            //            resUpdateRecord.Res_Khalti_Message = resDeposit.Particulars;
                                            //            resUpdateRecord.Res_Khalti_Id = resDeposit.RefferalsId;
                                            //            resUpdateRecord.Res_Khalti_State = AddDepositOrders.DepositStatus.Failed.ToString();
                                            //            resUpdateRecord.Res_Khalti_Status = false;
                                            //            resUpdateRecord.Res_Output = JsonConvert.SerializeObject(cres);
                                            //            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                                            //        }
                                            //        response.StatusCode = HttpStatusCode.BadRequest;
                                            //        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                            //        return response;
                                            //    }
                                            //}
                                            //else
                                            //{
                                            //    resDeposit.Status = (int)AddDepositOrders.DepositStatus.Failed;
                                            //    if (BankType != "NPS")
                                            //    {
                                            //        resDeposit.Particulars = res.cipsTxnResponseList[0].responseMessage;
                                            //        resDeposit.JsonResponse = data;
                                            //        resDeposit.ResponseCode = res.cipsTxnResponseList[0].responseCode;
                                            //    }
                                            //    else
                                            //    {
                                            //        resDeposit.Particulars = fundres.message;
                                            //        resDeposit.JsonResponse = JsonConvert.SerializeObject(fundres);
                                            //        resDeposit.ResponseCode = fundres.code;
                                            //    }
                                            //    RepCRUD<AddDepositOrders, GetDepositOrders>.Update(resDeposit, "depositorders");
                                            //    cres = CommonApiMethod.ReturnBadRequestMessage("Transaction Not found");
                                            //    AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                                            //    GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                                            //    inobjApiResponse.Id = resVendor_API_Requests.Id;
                                            //    AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                                            //    if (resUpdateRecord != null && resUpdateRecord.Id != 0)
                                            //    {
                                            //        resUpdateRecord.Req_Khalti_Input = JsonConvert.SerializeObject(bank);
                                            //        resUpdateRecord.Res_Khalti_Output = data;
                                            //        resUpdateRecord.Res_Khalti_Message = resDeposit.Particulars;
                                            //        resUpdateRecord.Res_Khalti_Id = resDeposit.RefferalsId;
                                            //        resUpdateRecord.Res_Khalti_State = AddDepositOrders.DepositStatus.Failed.ToString();
                                            //        resUpdateRecord.Res_Khalti_Status = false;
                                            //        resUpdateRecord.Res_Output = JsonConvert.SerializeObject(cres);
                                            //        RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                                            //    }

                                            //    response.StatusCode = HttpStatusCode.BadRequest;
                                            //    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                            //    return response;
                                            //}

                                        }
                                        else
                                        {
                                            //string msgresult = WalletRefund(instructionId, resuserdetails, authenticationToken, user.PlatForm, user.DeviceCode);

                                            AdminRemarksCD = "Remittance Refund Transaction.";
                                            TransactionType = "1";
                                            //for credit from merchat balance (refund)
                                            string refundtransactionresult = RepTransactions.MerchantWalletUpdateFromAdmin(model, TransactionAmount, Referenceno, TransactionType, ref UserMessage, AdminRemarksCD, "");


                                            resDeposit.Status = (int)AddDepositOrders.DepositStatus.Refund;
                                            if (BankType != "NPS")
                                            {
                                                resDeposit.Particulars = res.cipsTxnResponseList[0].responseMessage;
                                                resDeposit.JsonResponse = data;
                                                resDeposit.ResponseCode = res.cipsTxnResponseList[0].responseCode;
                                            }
                                            else
                                            {
                                                resDeposit.Particulars = fundres.message;
                                                resDeposit.JsonResponse = JsonConvert.SerializeObject(fundres);
                                                resDeposit.ResponseCode = fundres.code;
                                            }
                                            RepCRUD<AddDepositOrders, GetDepositOrders>.Update(resDeposit, "depositorders");
                                            //cres = CommonApiMethod.ReturnBadRequestMessage("Transaction Has Been Failed.Due To Some Reasons.Please Try Again!");
                                            cres = CommonApiMethod.ReturnBadRequestMessage(res.cipsBatchResponse.responseMessage + "," + resDeposit.Particulars);
                                            AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                                            GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                                            inobjApiResponse.Id = resVendor_API_Requests.Id;
                                            AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_RemittanceAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                                            if (resUpdateRecord != null && resUpdateRecord.Id != 0)
                                            {
                                                resUpdateRecord.Req_Khalti_Input = JsonConvert.SerializeObject(bank);
                                                if (BankType != "NPS")
                                                {
                                                    resUpdateRecord.Res_Khalti_Output = data;
                                                }
                                                else
                                                {
                                                    resUpdateRecord.Res_Khalti_Output = JsonConvert.SerializeObject(fundres);
                                                }
                                                resUpdateRecord.Res_Khalti_Message = refundtransactionresult;
                                                resUpdateRecord.Res_Khalti_Id = resDeposit.RefferalsId;
                                                resUpdateRecord.Res_Khalti_State = AddDepositOrders.DepositStatus.Refund.ToString();
                                                resUpdateRecord.Res_Khalti_Status = false;
                                                resUpdateRecord.Res_Output = JsonConvert.SerializeObject(cres);
                                                RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "remittance_api_requests");
                                            }

                                            response.StatusCode = HttpStatusCode.BadRequest;
                                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                            //Common.AddLogs(cres.Message, false, Convert.ToInt32(AddLog.LogType.BankTransfer), user.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, true, user.PlatForm, user.DeviceCode);

                                            return response;

                                        }
                                    }
                                    else
                                    {
                                        cres = CommonApiMethod.ReturnBadRequestMessage("Request Not Found");
                                        response.StatusCode = HttpStatusCode.BadRequest;
                                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);

                                        AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                                        GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                                        inobjApiResponse.Id = resVendor_API_Requests.Id;
                                        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_RemittanceAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                                        if (resUpdateRecord != null && resUpdateRecord.Id != 0)
                                        {
                                            resUpdateRecord.Req_Khalti_Input = JsonConvert.SerializeObject(bank);
                                            if (BankType != "NPS")
                                            {
                                                resUpdateRecord.Res_Khalti_Output = data;
                                            }
                                            else
                                            {
                                                resUpdateRecord.Res_Khalti_Output = JsonConvert.SerializeObject(fundres);
                                            }
                                            resUpdateRecord.Res_Khalti_Message = resDeposit.Particulars;
                                            resUpdateRecord.Res_Khalti_Id = resDeposit.RefferalsId;
                                            resUpdateRecord.Res_Khalti_State = AddDepositOrders.DepositStatus.Failed.ToString();
                                            resUpdateRecord.Res_Khalti_Status = false;
                                            resUpdateRecord.Res_Output = JsonConvert.SerializeObject(cres);
                                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "remittance_api_requests");
                                        }
                                        //Common.AddLogs(cres.Message, false, Convert.ToInt32(AddLog.LogType.BankTransfer), user.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, true, user.PlatForm, user.DeviceCode);

                                        return response;
                                    }

                                }
                                else
                                {
                                    cres = CommonApiMethod.ReturnBadRequestMessage("Data Not Found");
                                    AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                                    GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                                    inobjApiResponse.Id = resVendor_API_Requests.Id;
                                    AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_RemittanceAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                                    if (resUpdateRecord != null && resUpdateRecord.Id != 0)
                                    {
                                        resUpdateRecord.Req_Khalti_Input = JsonConvert.SerializeObject(bank);
                                        if (BankType != "NPS")
                                        {
                                            resUpdateRecord.Res_Khalti_Output = data;
                                        }
                                        else
                                        {
                                            resUpdateRecord.Res_Khalti_Output = JsonConvert.SerializeObject(fundres);
                                        }
                                        //resUpdateRecord.Res_Khalti_Message = resDeposit.Particulars;
                                        //resUpdateRecord.Res_Khalti_Id = resDeposit.RefferalsId;
                                        resUpdateRecord.Res_Khalti_State = AddDepositOrders.DepositStatus.Failed.ToString();
                                        resUpdateRecord.Res_Khalti_Status = false;
                                        resUpdateRecord.Res_Output = JsonConvert.SerializeObject(cres);
                                        RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "remittance_api_requests");
                                    }

                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                    //Common.AddLogs(cres.Message, false, Convert.ToInt32(AddLog.LogType.BankTransfer), user.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, true, user.PlatForm, user.DeviceCode);

                                    return response;
                                }
                            }
                            else
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage(transactionresult);

                                AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                                GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                                inobjApiResponse.Id = resVendor_API_Requests.Id;
                                AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_RemittanceAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                                if (resUpdateRecord != null && resUpdateRecord.Id != 0)
                                {
                                    resUpdateRecord.Req_Khalti_Input = JsonConvert.SerializeObject(bank);
                                    //resUpdateRecord.Res_Khalti_Output = data;
                                    //resUpdateRecord.Res_Khalti_Message = resDeposit.Particulars;
                                    //resUpdateRecord.Res_Khalti_Id = resDeposit.RefferalsId;
                                    resUpdateRecord.Res_Khalti_State = AddDepositOrders.DepositStatus.Failed.ToString();
                                    resUpdateRecord.Res_Khalti_Status = false;
                                    resUpdateRecord.Res_Output = JsonConvert.SerializeObject(cres);
                                    RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "remittance_api_requests");
                                }
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                //Common.AddLogs(cres.Message, false, Convert.ToInt32(AddLog.LogType.BankTransfer), user.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, true, user.PlatForm, user.DeviceCode);

                                return response;
                            }
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Something Went Wrong Try Again Later!");

                            AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                            GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                            inobjApiResponse.Id = resVendor_API_Requests.Id;
                            AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_RemittanceAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                            if (resUpdateRecord != null && resUpdateRecord.Id != 0)
                            {
                                resUpdateRecord.Req_Khalti_Input = JsonConvert.SerializeObject(bank);
                                //resUpdateRecord.Res_Khalti_Output = data;
                                //resUpdateRecord.Res_Khalti_Message = resDeposit.Particulars;
                                //resUpdateRecord.Res_Khalti_Id = resDeposit.RefferalsId;
                                resUpdateRecord.Res_Khalti_State = AddDepositOrders.DepositStatus.Failed.ToString();
                                resUpdateRecord.Res_Khalti_Status = false;
                                resUpdateRecord.Res_Output = JsonConvert.SerializeObject(cres);
                                RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "remittance_api_requests");
                            }
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            //Common.AddLogs(cres.Message, false, Convert.ToInt32(AddLog.LogType.BankTransfer), user.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, true, user.PlatForm, user.DeviceCode);

                            return response;
                        }


                    }
                    else
                    {
                        cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }



                    //return response;


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
                        Common.AddLogs(ve.ErrorMessage, false, 1, 10000, "", true);
                    }
                }
                log.Error($"{System.DateTime.Now.ToString()} BankTransfer {e.ToString()} " + Environment.NewLine);
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} BankTransfer {ex.ToString()} " + Environment.NewLine);
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
