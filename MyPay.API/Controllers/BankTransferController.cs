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

namespace MyPay.API.Controllers
{
    public class BankTransferController : ApiController
    {

        private static ILog log = LogManager.GetLogger(typeof(BankTransferController));


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/BankTransfer")]
        public HttpResponseMessage BankTransfer(Req_BankTransfer user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside BankTransfer" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            var response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);

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
                        //string UserInput = getRawPostData().Result;
                        string CommonResult = "";
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString().Trim() != "0")
                        {
                            int VendorAPIType = 0;
                            int Type = 0;
                            Int64 memId = Convert.ToInt64(user.MemberId);

                            VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.bank_transfer;
                            Type = (int)VendorApi_CommonHelper.VendorTypes.khalti;

                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, true, user.Amount.ToString(), true, user.Mpin, "", false, true);
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

                        if (string.IsNullOrEmpty(user.AccountNumber))
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Please enter Bank Account Number");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        else if (string.IsNullOrEmpty(user.BankId))
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Please select Bank");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        else if (string.IsNullOrEmpty(user.BankName))
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Please Enter Bank Name");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        else if (string.IsNullOrEmpty(user.BranchName))
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Please Enter Branch Name");
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
                        else if (string.IsNullOrEmpty(user.BranchId))
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Please Select Branch");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        else if (string.IsNullOrEmpty(user.BankId))
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Please Select Bank");
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
                        else if (user.MemberId == null || user.MemberId == 0)
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Please Enter MemberId");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
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
                        else if (resuserdetails.IsKYCApproved != (int)AddUser.kyc.Verified)
                        {
                            string result1s = "Please verified your kyc first";
                            cres = CommonApiMethod.ReturnBadRequestMessage(result1s);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }


                        log.Info($"{System.DateTime.Now.ToString()} BankTransfer SaveAPIRequest start" + Environment.NewLine);
                        // *** SAVE API REQUEST
                        #region SaveAPIRequest
                        string ApiResponse = string.Empty;

                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.bank_transfer;
                        AddVendor_API_Requests resVendor_API_Requests = new AddVendor_API_Requests();
                        string instructionId = DateTime.UtcNow.ToString("ddMMyyyyhhmmss") + Common.RandomNumber(1111, 9999);
                        string Req_ReferenceNo = instructionId;
                        string JsonReq = string.Empty;
                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        resVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse("", Req_ReferenceNo, resuserdetails.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, JsonReq, authenticationToken, userInput, user.DeviceCode, user.PlatForm, VendorApiType);
                        #endregion

                        AddCalculateServiceChargeAndCashback objOut = MyPay.Models.Common.Common.CalculateNetAmountWithServiceCharge(user.MemberId.ToString(), user.Amount.ToString(), ((int)VendorApi_CommonHelper.KhaltiAPIName.bank_transfer).ToString());
                        if (resuserdetails.TotalAmount < decimal.Parse(objOut.NetAmount.ToString()))
                        {
                            string result1s = "Insufficient Balance";
                            Common.AddLogs(result1s, false, Convert.ToInt32(AddLog.LogType.BankTransfer), user.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, true, user.PlatForm, user.DeviceCode);

                            cres = CommonApiMethod.ReturnBadRequestMessage(result1s);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                            AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                            GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                            inobjApiResponse.Id = resVendor_API_Requests.Id;
                            AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                            if (resUpdateRecord != null && resUpdateRecord.Id != 0)
                            {
                                resUpdateRecord.Res_Output = ApiResponse;
                                resUpdateRecord.Req_Khalti_Input = resUpdateRecord.Req_Input;
                                resUpdateRecord.Res_Khalti_Output = resUpdateRecord.Res_Output;

                                RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                            }
                            return response;
                        }
                        string msg = Common.CheckTransactionLimit((int)VendorApi_CommonHelper.KhaltiAPIName.bank_transfer, (int)AddTransactionLimit.TransactionTransferTypeEnum.Bank_Transfer_From_Wallet, user.MemberId, objOut.NetAmount).ToLower();
                        if (msg != "success")
                        {
                            Common.AddLogs(msg, false, Convert.ToInt32(AddLog.LogType.BankTransfer), user.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, true, user.PlatForm, user.DeviceCode);

                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(response);
                            AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                            GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                            inobjApiResponse.Id = resVendor_API_Requests.Id;
                            AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                            if (resUpdateRecord != null && resUpdateRecord.Id != 0)
                            {
                                resUpdateRecord.Res_Output = ApiResponse;
                                RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                            }
                            return response;
                        }
                        else
                        {
                            AddNCBankTransfer bank = new AddNCBankTransfer();
                            bank.cipsBatchDetail.batchId = DateTime.UtcNow.ToString("ddMMyyyyhhmmss") + Common.RandomNumber(1111, 9999);
                            bank.cipsBatchDetail.batchAmount = Convert.ToDecimal(user.Amount).ToString("f2");
                            bank.cipsBatchDetail.batchCount = 1;
                            bank.cipsBatchDetail.batchCrncy = "NPR";
                            bank.cipsBatchDetail.categoryPurpose = "ECPG";
                            bank.cipsBatchDetail.debtorAgent = Common.ConnectIps_BankId;
                            bank.cipsBatchDetail.debtorBranch = Common.ConnectIPs_BranchId;
                            bank.cipsBatchDetail.debtorName = Common.ConnectIps_AccountName;
                            bank.cipsBatchDetail.debtorAccount = Common.ConnectIPs_AccountNumber;
                            string batch = bank.cipsBatchDetail.batchId + "," + bank.cipsBatchDetail.debtorAgent + "," + bank.cipsBatchDetail.debtorBranch + "," + bank.cipsBatchDetail.debtorAccount + "," + bank.cipsBatchDetail.batchAmount + "," + bank.cipsBatchDetail.batchCrncy;

                            AddNCTransactionDetail transactionlist = new AddNCTransactionDetail();

                            transactionlist.instructionId = instructionId;
                            transactionlist.endToEndId = Common.TruncateLongString(user.Description, 29);
                            transactionlist.amount = Convert.ToDecimal(user.Amount).ToString("f2");
                            transactionlist.creditorAgent = user.BankId;
                            transactionlist.creditorBranch = user.BranchId;
                            transactionlist.creditorName = user.Name;
                            transactionlist.creditorAccount = user.AccountNumber;
                            transactionlist.freeCode1 = user.MemberId.ToString();
                            bank.cipsTransactionDetailList.Add(transactionlist);
                            string transaction = bank.cipsTransactionDetailList[0].instructionId + "," + bank.cipsTransactionDetailList[0].creditorAgent + "," + bank.cipsTransactionDetailList[0].creditorBranch + "," + bank.cipsTransactionDetailList[0].creditorAccount + "," + bank.cipsTransactionDetailList[0].amount;

                            bank.token = Common.GenerateConnectIPSToken(batch + "," + transaction + "," + RepNCHL.withdrawusername);
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
                            outobjectver.CreatedBy = Common.GetCreatedById(authenticationToken);
                            outobjectver.CreatedByName = Common.GetCreatedByName(authenticationToken);
                            outobjectver.TransactionId = bank.cipsBatchDetail.batchId;
                            outobjectver.RefferalsId = bank.cipsTransactionDetailList[0].instructionId;
                            outobjectver.MemberId = Convert.ToInt64(resuserdetails.MemberId);
                            outobjectver.Type = (int)AddDepositOrders.DepositType.Bank_Transfer;
                            outobjectver.Remarks = Common.TruncateLongString(user.Description, 29);
                            outobjectver.Particulars = "Bank Transfer Initiate";
                            outobjectver.Status = (int)AddDepositOrders.DepositStatus.Incomplete;
                            outobjectver.IsActive = true;
                            outobjectver.IsApprovedByAdmin = true;
                            outobjectver.ServiceCharges = objOut.ServiceCharge;
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
                                string transactionresult = WalletDeduct(outobjectver, resuserdetails, bank, authenticationToken, Common.TruncateLongString(user.Description, 29), user.BankName, user.BranchName, resVendor_API_Requests, user.PlatForm, user.DeviceCode, objOut, BankType);

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
                                        fundres = RepNps.FundTransferRequest(user.Name, user.AccountNumber, user.BankId, bank.cipsBatchDetail.batchId, bank.cipsTransactionDetailList[0].instructionId, user.Amount.ToString(), user.Description);
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
                                                AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
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
                                                    RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                                                }
                                                Common.AddLogs(resDeposit.Particulars, false, Convert.ToInt32(AddLog.LogType.BankTransfer), user.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, true, user.PlatForm, user.DeviceCode);
                                                string msgresult = WalletRefund(instructionId, resuserdetails, authenticationToken, user.PlatForm, user.DeviceCode);

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
                                                if (restra.GetRecord())
                                                {
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

                                                    if (restra.Update())
                                                    {
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
                                                        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
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
                                                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                                                        }

                                                        #region SendEmailConfirmation
                                                        string mystring = File.ReadAllText(HttpContext.Current.Server.MapPath("/Templates/BankTransfer.html"));
                                                        string body = mystring;
                                                        body = body.Replace("##Amount##", (restra.Amount).ToString("0.00"));
                                                        body = body.Replace("##TransactionId##", restra.TransactionUniqueId);
                                                        body = body.Replace("##Date##", restra.CreatedDate.ToString("dd-MMM-yyyy hh:mm:ss tt"));
                                                        body = body.Replace("##Type##", "Bank Transfer");
                                                        body = body.Replace("##Status##", WalletTransactions.Statuses.Success.ToString());
                                                        body = body.Replace("##Cashback##", objOut.CashbackAmount.ToString("0.00"));
                                                        body = body.Replace("##ServiceCharge##", objOut.ServiceCharge.ToString("0.00"));
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
                                                        response.StatusCode = HttpStatusCode.Created;
                                                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.Created, cres);
                                                        log.Info($"{System.DateTime.Now.ToString()} BankTransfer completed" + Environment.NewLine);
                                                        return response;

                                                    }
                                                    else
                                                    {
                                                        resDeposit.Status = (int)AddDepositOrders.DepositStatus.Failed;
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
                                                        cres = CommonApiMethod.ReturnBadRequestMessage("Transaction Not Update");
                                                        AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                                                        GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                                                        inobjApiResponse.Id = resVendor_API_Requests.Id;
                                                        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                                                        if (resUpdateRecord != null && resUpdateRecord.Id != 0)
                                                        {
                                                            resUpdateRecord.Req_Khalti_Input = JsonConvert.SerializeObject(bank);
                                                            resUpdateRecord.Res_Khalti_Output = data;
                                                            resUpdateRecord.Res_Khalti_Message = resDeposit.Particulars;
                                                            resUpdateRecord.Res_Khalti_Id = resDeposit.RefferalsId;
                                                            resUpdateRecord.Res_Khalti_State = AddDepositOrders.DepositStatus.Failed.ToString();
                                                            resUpdateRecord.Res_Khalti_Status = false;
                                                            resUpdateRecord.Res_Output = JsonConvert.SerializeObject(cres);
                                                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                                                        }
                                                        response.StatusCode = HttpStatusCode.BadRequest;
                                                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                                        return response;
                                                    }
                                                }
                                                else
                                                {
                                                    resDeposit.Status = (int)AddDepositOrders.DepositStatus.Failed;
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
                                                    cres = CommonApiMethod.ReturnBadRequestMessage("Transaction Not found");
                                                    AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                                                    GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                                                    inobjApiResponse.Id = resVendor_API_Requests.Id;
                                                    AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                                                    if (resUpdateRecord != null && resUpdateRecord.Id != 0)
                                                    {
                                                        resUpdateRecord.Req_Khalti_Input = JsonConvert.SerializeObject(bank);
                                                        resUpdateRecord.Res_Khalti_Output = data;
                                                        resUpdateRecord.Res_Khalti_Message = resDeposit.Particulars;
                                                        resUpdateRecord.Res_Khalti_Id = resDeposit.RefferalsId;
                                                        resUpdateRecord.Res_Khalti_State = AddDepositOrders.DepositStatus.Failed.ToString();
                                                        resUpdateRecord.Res_Khalti_Status = false;
                                                        resUpdateRecord.Res_Output = JsonConvert.SerializeObject(cres);
                                                        RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                                                    }

                                                    response.StatusCode = HttpStatusCode.BadRequest;
                                                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                                    return response;
                                                }
                                            }
                                            else
                                            {
                                                string msgresult = WalletRefund(instructionId, resuserdetails, authenticationToken, user.PlatForm, user.DeviceCode);

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
                                                cres = CommonApiMethod.ReturnBadRequestMessage("Transaction Has Been Failed.Due To Some Reasons.Please Try Again!");
                                                AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                                                GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                                                inobjApiResponse.Id = resVendor_API_Requests.Id;
                                                AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
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
                                                    resUpdateRecord.Res_Khalti_Message = msgresult;
                                                    resUpdateRecord.Res_Khalti_Id = resDeposit.RefferalsId;
                                                    resUpdateRecord.Res_Khalti_State = AddDepositOrders.DepositStatus.Refund.ToString();
                                                    resUpdateRecord.Res_Khalti_Status = false;
                                                    resUpdateRecord.Res_Output = JsonConvert.SerializeObject(cres);
                                                    RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                                                }

                                                response.StatusCode = HttpStatusCode.BadRequest;
                                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                                Common.AddLogs(cres.Message, false, Convert.ToInt32(AddLog.LogType.BankTransfer), user.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, true, user.PlatForm, user.DeviceCode);

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
                                            AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
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
                                                RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                                            }
                                            Common.AddLogs(cres.Message, false, Convert.ToInt32(AddLog.LogType.BankTransfer), user.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, true, user.PlatForm, user.DeviceCode);

                                            return response;
                                        }

                                    }
                                    else
                                    {
                                        cres = CommonApiMethod.ReturnBadRequestMessage("Data Not Found");
                                        AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                                        GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                                        inobjApiResponse.Id = resVendor_API_Requests.Id;
                                        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
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
                                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                                        }

                                        response.StatusCode = HttpStatusCode.BadRequest;
                                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                        Common.AddLogs(cres.Message, false, Convert.ToInt32(AddLog.LogType.BankTransfer), user.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, true, user.PlatForm, user.DeviceCode);

                                        return response;
                                    }
                                }
                                else
                                {
                                    cres = CommonApiMethod.ReturnBadRequestMessage(transactionresult);

                                    AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                                    GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                                    inobjApiResponse.Id = resVendor_API_Requests.Id;
                                    AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                                    if (resUpdateRecord != null && resUpdateRecord.Id != 0)
                                    {
                                        resUpdateRecord.Req_Khalti_Input = JsonConvert.SerializeObject(bank);
                                        //resUpdateRecord.Res_Khalti_Output = data;
                                        //resUpdateRecord.Res_Khalti_Message = resDeposit.Particulars;
                                        //resUpdateRecord.Res_Khalti_Id = resDeposit.RefferalsId;
                                        resUpdateRecord.Res_Khalti_State = AddDepositOrders.DepositStatus.Failed.ToString();
                                        resUpdateRecord.Res_Khalti_Status = false;
                                        resUpdateRecord.Res_Output = JsonConvert.SerializeObject(cres);
                                        RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                                    }
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                    Common.AddLogs(cres.Message, false, Convert.ToInt32(AddLog.LogType.BankTransfer), user.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, true, user.PlatForm, user.DeviceCode);

                                    return response;
                                }
                            }
                            else
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage("Something Went Wrong Try Again Later!");

                                AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                                GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                                inobjApiResponse.Id = resVendor_API_Requests.Id;
                                AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                                if (resUpdateRecord != null && resUpdateRecord.Id != 0)
                                {
                                    resUpdateRecord.Req_Khalti_Input = JsonConvert.SerializeObject(bank);
                                    //resUpdateRecord.Res_Khalti_Output = data;
                                    //resUpdateRecord.Res_Khalti_Message = resDeposit.Particulars;
                                    //resUpdateRecord.Res_Khalti_Id = resDeposit.RefferalsId;
                                    resUpdateRecord.Res_Khalti_State = AddDepositOrders.DepositStatus.Failed.ToString();
                                    resUpdateRecord.Res_Khalti_Status = false;
                                    resUpdateRecord.Res_Output = JsonConvert.SerializeObject(cres);
                                    RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                                }
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                Common.AddLogs(cres.Message, false, Convert.ToInt32(AddLog.LogType.BankTransfer), user.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, true, user.PlatForm, user.DeviceCode);

                                return response;
                            }
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

        private static string WalletDeduct(AddDepositOrders resDeposit, AddUserLoginWithPin resuserdetails, AddNCBankTransfer bank, string authenticationToken, string Description, string BankName, string BranchName, AddVendor_API_Requests resVendor_API_Requests, string PlatForm, string DeviceCode, AddCalculateServiceChargeAndCashback objOut, string BankType)
        {
            log.Info($"{System.DateTime.Now.ToString()} BankTransfer WalletDeduct start" + Environment.NewLine);
            string result = "";
            try
            {
                decimal WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resuserdetails.TotalAmount) - (Convert.ToDecimal(bank.cipsBatchDetail.batchAmount) + objOut.ServiceCharge));
                WalletTransactions res_transaction = new WalletTransactions();
                res_transaction.TransactionUniqueId = bank.cipsTransactionDetailList[0].instructionId;
                if (!res_transaction.GetRecordCheckExists())
                {

                    res_transaction.MemberId = Convert.ToInt64(resuserdetails.MemberId);
                    res_transaction.ContactNumber = resuserdetails.ContactNumber;
                    res_transaction.MemberName = resuserdetails.FirstName + " " + resuserdetails.MiddleName + " " + resuserdetails.LastName;
                    res_transaction.Amount = Convert.ToDecimal(bank.cipsBatchDetail.batchAmount);
                    res_transaction.UpdateBy = Convert.ToInt64(resuserdetails.MemberId);
                    res_transaction.UpdateByName = resuserdetails.FirstName + " " + resuserdetails.MiddleName + " " + resuserdetails.LastName;
                    res_transaction.CurrentBalance = WalletBalance;
                    res_transaction.CreatedBy = Common.GetCreatedById(authenticationToken);
                    res_transaction.CreatedByName = Common.GetCreatedByName(authenticationToken);
                    res_transaction.TransactionUniqueId = bank.cipsTransactionDetailList[0].instructionId;
                    res_transaction.VendorTransactionId = bank.cipsTransactionDetailList[0].instructionId;
                    res_transaction.Reference = bank.cipsBatchDetail.batchId;
                    //res_transaction.BatchTransactionId = res.cipsBatchResponse.id;
                    //res_transaction.TxnInstructionId = res.cipsTxnResponseList[0].id;
                    res_transaction.Remarks = "Payment Successfully Sent";
                    res_transaction.Purpose = Description;
                    res_transaction.GatewayStatus = WalletTransactions.Statuses.Pending.ToString();
                    res_transaction.ResponseCode = "-1";
                    res_transaction.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.bank_transfer;
                    res_transaction.Description = bank.cipsTransactionDetailList[0].endToEndId;
                    res_transaction.Status = (int)WalletTransactions.Statuses.Pending;

                    res_transaction.IsApprovedByAdmin = true;
                    res_transaction.IsActive = true;
                    res_transaction.Sign = Convert.ToInt16(WalletTransactions.Signs.Debit);
                    res_transaction.RecieverName = bank.cipsTransactionDetailList[0].creditorName;
                    res_transaction.TransferType = (int)WalletTransactions.TransferTypes.Sender;
                    res_transaction.RecieverAccountNo = bank.cipsTransactionDetailList[0].creditorAccount;
                    res_transaction.RecieverBankCode = bank.cipsTransactionDetailList[0].creditorAgent;
                    res_transaction.RecieverBranch = bank.cipsTransactionDetailList[0].creditorBranch;
                    res_transaction.SenderAccountNo = bank.cipsBatchDetail.debtorAccount;
                    res_transaction.SenderBankCode = bank.cipsBatchDetail.debtorAgent;
                    res_transaction.SenderBranch = bank.cipsBatchDetail.debtorBranch;
                    res_transaction.ServiceCharge = objOut.ServiceCharge;
                    res_transaction.NetAmount = Convert.ToDecimal(bank.cipsBatchDetail.batchAmount) + objOut.ServiceCharge;
                    res_transaction.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
                    res_transaction.SenderBankName = Common.ConnectIPs_BankName;
                    res_transaction.SenderBranchName = Common.ConnectIPs_BranchName;
                    res_transaction.RecieverBankName = BankName;
                    res_transaction.RecieverBranchName = BranchName;
                    if (BankType != "NPS")
                    {
                        res_transaction.VendorType = (int)VendorApi_CommonHelper.VendorTypes.NCHL;
                    }
                    else
                    {
                        res_transaction.VendorType = (int)VendorApi_CommonHelper.VendorTypes.NPS;
                    }

                    res_transaction.Platform = PlatForm;
                    res_transaction.DeviceCode = DeviceCode;
                    if (res_transaction.Add())
                    {

                        result = "success";


                    }
                    else
                    {
                        result = "Something Went Wrong Payment Not Sent";

                    }
                }
                else
                {
                    result = "Transaction Sent Already";

                }

            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} BankTransfer WalletDeduct {ex.ToString()} " + Environment.NewLine);
                result = ex.Message;
            }
            Common.AddLogs("BankTransfer:" + result, false, Convert.ToInt32(AddLog.LogType.BankTransfer), resuserdetails.MemberId, resuserdetails.FirstName + " " + resuserdetails.LastName, true, PlatForm, DeviceCode);
            log.Info($"{System.DateTime.Now.ToString()} BankTransfer WalletDeduct complete" + Environment.NewLine);
            return result;
        }
        private string WalletRefund(string TransactionUniqueId, AddUserLoginWithPin resuser, string authenticationToken, string Platform, string DeviceCode)
        {
            log.Info($"{System.DateTime.Now.ToString()} BankTransfer WalletRefund start" + Environment.NewLine);
            string result = "";
            try
            {
                AddUserBasicInfo resuserdetails = new AddUserBasicInfo();
                resuserdetails.MemberId = resuser.MemberId;
                resuserdetails.GetUserInformationBasic();

                if (resuserdetails.Id > 0)
                {
                    WalletTransactions res_transaction = new WalletTransactions();
                    res_transaction.TransactionUniqueId = TransactionUniqueId;
                    if (res_transaction.GetRecord())
                    {
                        WalletTransactions newtra = new WalletTransactions();
                        decimal WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resuserdetails.TotalAmount) + (Convert.ToDecimal(res_transaction.Amount)) + (Convert.ToDecimal(res_transaction.ServiceCharge)));
                        newtra.MemberId = Convert.ToInt64(resuserdetails.MemberId);
                        newtra.ContactNumber = resuserdetails.ContactNumber;
                        newtra.MemberName = resuserdetails.FirstName + " " + resuserdetails.LastName;
                        newtra.Amount = Convert.ToDecimal(res_transaction.Amount) + res_transaction.ServiceCharge;
                        newtra.UpdateBy = Convert.ToInt64(resuserdetails.MemberId);
                        newtra.UpdateByName = resuserdetails.FirstName + " " + resuserdetails.LastName;
                        newtra.CurrentBalance = WalletBalance;
                        newtra.CreatedBy = Common.GetCreatedById(authenticationToken);
                        newtra.CreatedByName = Common.GetCreatedByName(authenticationToken);
                        newtra.TransactionUniqueId = new CommonHelpers().GenerateUniqueId();
                        newtra.VendorTransactionId = new CommonHelpers().GenerateUniqueId();
                        newtra.ParentTransactionId = TransactionUniqueId;
                        newtra.Reference = res_transaction.Reference;
                        newtra.BatchTransactionId = res_transaction.BatchTransactionId;
                        newtra.TxnInstructionId = res_transaction.TxnInstructionId;
                        newtra.Description = "Wallet Credited Successfully";
                        newtra.Remarks = $"Wallet Credit as Refund for Txn id:{res_transaction.TransactionUniqueId}";
                        newtra.Purpose = "";
                        newtra.GatewayStatus = WalletTransactions.Statuses.Refund.ToString();
                        newtra.ResponseCode = "-1";
                        newtra.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.bank_transfer;
                        newtra.Status = (int)WalletTransactions.Statuses.Refund;
                        newtra.IsApprovedByAdmin = true;
                        newtra.IsActive = true;
                        newtra.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
                        newtra.RecieverName = res_transaction.RecieverName;
                        newtra.TransferType = (int)WalletTransactions.TransferTypes.Sender;
                        newtra.RecieverAccountNo = res_transaction.RecieverAccountNo;
                        newtra.RecieverBankCode = res_transaction.RecieverBankCode;
                        newtra.RecieverBranch = res_transaction.RecieverBranch;
                        newtra.SenderAccountNo = res_transaction.SenderAccountNo;
                        newtra.SenderBankCode = res_transaction.SenderBankCode;
                        newtra.SenderBranch = res_transaction.SenderBranch;
                        newtra.ServiceCharge = res_transaction.ServiceCharge;
                        newtra.NetAmount = Convert.ToDecimal(res_transaction.Amount) + res_transaction.ServiceCharge;
                        newtra.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
                        newtra.SenderBankName = Common.ConnectIPs_BankName;
                        newtra.SenderBranchName = Common.ConnectIPs_BranchName;
                        newtra.RecieverBankName = res_transaction.RecieverBankName;
                        newtra.RecieverBranchName = res_transaction.RecieverBranchName;
                        newtra.VendorType = (int)VendorApi_CommonHelper.VendorTypes.MyPay;
                        newtra.Platform = res_transaction.Platform;
                        newtra.DeviceCode = res_transaction.DeviceCode;
                        if (newtra.Add())
                        {
                            res_transaction.Status = (int)WalletTransactions.Statuses.Failed;
                            res_transaction.GatewayStatus = WalletTransactions.Statuses.Failed.ToString();
                            if (res_transaction.Update())
                            {
                                result = "success";
                            }
                            else
                            {
                                result = "Refunded But Transaction Not Updated";
                            }

                        }
                        else
                        {
                            result = "Something Went Wrong Payment Not Sent";
                        }
                    }
                    else
                    {
                        result = "Transaction Not Found For Refund";
                    }
                }
                else
                {
                    result = "Transaction User Not Found For Refund";
                }
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} BankTransfer WalletRefund {ex.ToString()} " + Environment.NewLine);
                result = ex.Message;
            }
            Common.AddLogs("Refund Bank Transfer:" + result, false, Convert.ToInt32(AddLog.LogType.BankTransfer), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, Platform, DeviceCode);
            log.Info($"{System.DateTime.Now.ToString()} BankTransfer WalletRefund complete" + Environment.NewLine);
            return result;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/get-cipstransaction-batchid")]
        public HttpResponseMessage GetCipsTransactionByBatchID(Req_ConnectIPSLookupBatch user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside get-cipstransaction-batchid" + Environment.NewLine);
            string ApiResponse = string.Empty;
            CommonResponse cres = new CommonResponse();
            Res_GetCipsLookupResponse result = new Res_GetCipsLookupResponse();
            var response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);

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
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString().Trim() != "0")
                        {
                            int VendorAPIType = 0;
                            int Type = 0;
                            Int64 memId = Convert.ToInt64(user.MemberId);

                            VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.bank_transfer;
                            Type = (int)VendorApi_CommonHelper.VendorTypes.khalti;

                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin);
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
                        string msg = String.Empty;
                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        CipsBatchDetail objRes = new CipsBatchDetail();

                        msg = RepNCHL.CipsStatusJSONResponseProcess(user.BATCHID, user.MemberId.ToString(), authenticationToken, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                        if (msg.ToLower() == "success")
                        {
                            result.Message = "Success";
                            result.Details = objRes.batchId;
                            result.ReponseCode = 1;
                            result.status = true;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_GetCipsLookupResponse>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} get-cipstransaction-batchid completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} get-cipstransaction-batchid {e.ToString()} " + Environment.NewLine);
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} get-cipstransaction-batchid {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/get-cipstransaction-batchid-all")]
        public HttpResponseMessage GetCipsTransactionByBatchIDAll(Req_ConnectIPSLookupBatch user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside get-cipstransaction-batchid-all" + Environment.NewLine);
            string ApiResponse = string.Empty;
            CommonResponse cres = new CommonResponse();
            Res_GetCipsLookupResponse result = new Res_GetCipsLookupResponse();
            var response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);

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
                        AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString().Trim() != "0")
                        {
                            int VendorAPIType = 0;
                            int Type = 0;
                            Int64 memId = Convert.ToInt64(user.MemberId);

                            VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.bank_transfer;
                            Type = (int)VendorApi_CommonHelper.VendorTypes.khalti;

                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin);
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

                        string msg = String.Empty;
                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        AddDepositOrders outobject = new AddDepositOrders();
                        GetDepositOrders inobject = new GetDepositOrders();
                        inobject.Status = (int)AddDepositOrders.DepositStatus.Incomplete;
                        inobject.Type = (int)AddDepositOrders.DepositType.Bank_Transfer;
                        List<AddDepositOrders> resDepositList = RepCRUD<GetDepositOrders, AddDepositOrders>.GetRecordList(nameof(Common.StoreProcedures.sp_DepositOrders_Get), inobject, outobject);
                        CipsBatchDetail objRes = new CipsBatchDetail();
                        if (resDepositList.Count > 0)
                        {
                            foreach (var resDeposit in resDepositList)
                            {
                                msg = RepNCHL.CipsStatusJSONResponseProcess(resDeposit.TransactionId, resDeposit.MemberId.ToString(), authenticationToken, user.Version, user.DeviceCode, user.PlatForm, ref objRes);
                            }
                        }
                        if (msg.ToLower() == "success")
                        {
                            result.Message = "Success";
                            result.Details = objRes.batchId;
                            result.ReponseCode = 1;
                            result.status = true;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_GetCipsLookupResponse>(System.Net.HttpStatusCode.OK, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} get-cipstransaction-batchid-all completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} get-cipstransaction-batchid-all {e.ToString()} " + Environment.NewLine);
                throw;
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} get-cipstransaction-batchid-all {ex.ToString()} " + Environment.NewLine);
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