using MyPay.API.CustomFrameworkClasses;
using MyPay.API.Models;
using MyPay.API.Models.NPS;
using MyPay.API.Models.NPS.Response.Consumer;
using MyPay.API.Models.NPS.Response.Partner;
using MyPay.API.Models.Request.NPS.Consumer;
using MyPay.API.Models.Request.NPS.Partner;
using MyPay.API.Models.Request.Voting.Consumer;
using MyPay.API.Models.Response.Voting.Partner;
using MyPay.API.Repository;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using log4net;
using Antlr.Runtime;
using MyPay.Models.Get;
using System.Web.Util;
using MyPay.API.Models.Coupons;
using MyPay.Models.Miscellaneous;
using System.Web;
using log4net;
using Swashbuckle.Swagger;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using static iText.IO.Image.Jpeg2000ImageData;
using MyPay.API.Models.State;
using MyPay.Models.Response;
using System.Transactions;

namespace MyPay.API.Controllers.NPS
{

    
           


    public class NCHLParameters : CommonResponse
    {
        public FiscalYear fiscalYear;
        public NPSAreaCodes areaCode;
    }

    public class NCHLParametersReq: CommonProp
    {
        public string vendorAPIType;
    }
    public class NCHLController : MyPayAPIController
    {
        NCHLReqMapper requestMapper = new NCHLReqMapper();
        NCHLResponseMapper respMapper = new NCHLResponseMapper();
        NPSAreaCodes areaCodes = new NPSAreaCodes();
        FiscalYear fiscal = new FiscalYear();
        Res_CreditCardIsuuerList CCIssuerListResp = new Res_CreditCardIsuuerList();
        GetCreditCardIssuerList CCIssuersList = new GetCreditCardIssuerList();

        string NCHLBaseURL = Common.ApplicationEnvironment.IsProduction ? RepNCHL.NCHLWithdrawAPIURl : "http://demo.connectips.com:6065/";

        //public void addKeyValueToHash<T>(ref Hashtable ht, string key, T value)
        //{
        //    ht.Add(key, value);
        //}

        //public string getJSONfromHashTable(Hashtable ht) { 
        //    var JSONData = JsonConvert.SerializeObject(ht);
        //    return JSONData.ToString();
        //}

        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public NCHLController()
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            log.Info("Inside NCHLController constructor");


            try
            {
                var reqJSON = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/Models/NPS/NCHLReqMapping.json"));
                requestMapper = Newtonsoft.Json.JsonConvert.DeserializeObject<NCHLReqMapper>(reqJSON);

                if (requestMapper != null)
                {

                }

                var respJSON = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/Models/NPS/NCHLRespMapping.json"));
                respMapper = Newtonsoft.Json.JsonConvert.DeserializeObject<NCHLResponseMapper>(respJSON);


                var areaCodeFromFile = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/NCHLAreaCodes.json"));
                areaCodes = Newtonsoft.Json.JsonConvert.DeserializeObject<NPSAreaCodes>(areaCodeFromFile);

                var fiscalYearFromFile = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/NCHLFiscalYear.json"));
                fiscal = Newtonsoft.Json.JsonConvert.DeserializeObject<FiscalYear>(fiscalYearFromFile);


                //For Credit card
                var CCIssuersFromFile = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/NCHLCreditCardIssuer.json"));
                CCIssuerListResp = Newtonsoft.Json.JsonConvert.DeserializeObject<Res_CreditCardIsuuerList>(CCIssuersFromFile);
            }
            catch (Exception ex)
            {
                log.Info(ex.ToString());
                throw;
            }

            
            //Res_CreditCardIsuuerList
          //  CCIssuerListResp.Data = CCIssuersList;

        }

        //[HttpPost]
        //[Route("api/getNCHLAreaCodes")]

        //public HttpResponseMessage getNCHLAreaCodes()
        //{
        //    var response = Request.CreateResponse<NPSAreaCodes>(HttpStatusCode.OK, areaCodes);
        //    return response;
        //}




        [HttpPost]
        [Route("api/getNCHLAdditionalParameters")]

        public HttpResponseMessage getNCHLAdditionalParameters(NCHLParametersReq req)
        {
            
            CommonResponse cres = new CommonResponse();
            cres.status = false;
            cres.ReponseCode = 3;
            cres.Message = "Failed to get Govt services parameters";
            cres.responseMessage = "Failed to get Govt services parameters";

            if (req.vendorAPIType == "151")
            {
                var parameters = new NCHLParameters();
                parameters.fiscalYear = fiscal;
                parameters.areaCode = areaCodes;

                parameters.status = true;
                parameters.ReponseCode = 1;
                parameters.Message = "success";
                parameters.responseMessage = "success";

                var response = Request.CreateResponse<NCHLParameters>(HttpStatusCode.OK, parameters);
                return response;
            }

            var errorResp = Request.CreateResponse<CommonResponse>(HttpStatusCode.BadRequest, cres);
            return errorResp;
        }


        [HttpPost]
        [Route("api/NCHLLodgeRequest")]

        public HttpResponseMessage NCHLLodgeRequest(NCHLConsumerLodgeReq req)
        {

            log.Info("Inside NCHL lodge request");

   

            var response = Request.CreateResponse<String>(System.Net.HttpStatusCode.InternalServerError, "Couldnt process request");

           

            try
            {

                if (req.vendorAPIType == 151)
                {
                    req.chitNo = req.chitNo.ToString().PadLeft(12, '0');
                }

                string UserInput = getRawPostData().Result;

            var validationResponse = performGenericValidation(req, UserInput, true);
            
            if (validationResponse.StatusCode != HttpStatusCode.OK)
            {
                return validationResponse;
            }

            log.Info("NCHL lodge request generic validation passed");

            string NCHLLodgeBillEP = "api/billpayment/lodgebillpay.do";
            string NCHLURL = NCHLBaseURL + NCHLLodgeBillEP;
            string ApiResponse = string.Empty;

            AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
            int VendorApiType = req.vendorAPIType;
            objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse(NCHLURL, req.UniqueCustomerId, Common.CreatedBy, Common.CreatedByName, "", "", UserInput, req.DeviceCode, req.PlatForm, VendorApiType, "", "", "", ((int)VendorApi_CommonHelper.VendorTypes.MyPay).ToString());


            var lodgeReq = convertToNPSLodgeReq(req, req.vendorAPIType);
            lodgeReq.generateToken();

                log.Info("token generation ended");

            var lodgeBillReqDB = new LodgeBill();
            lodgeBillReqDB.batchId = lodgeReq.cipsBatchDetail.batchId;
            lodgeBillReqDB.instructionId = lodgeReq.cipsTransactionDetail.instructionId;
            lodgeBillReqDB.endToEndId = lodgeReq.cipsTransactionDetail.endToEndId;
            lodgeBillReqDB.amount = lodgeReq.cipsTransactionDetail.amount.ToString();
            lodgeBillReqDB.appId = lodgeReq.cipsTransactionDetail.appId;
            lodgeBillReqDB.confirmRequest = "";
            lodgeBillReqDB.confirmResponse = "";
            lodgeBillReqDB.lodgeRequest = JsonConvert.SerializeObject(lodgeReq);
            //lodgeBillReqDB.lodgeResponse = JsonConvert.SerializeObject(resObj);
            lodgeBillReqDB.memberId = req.MemberId;
            lodgeBillReqDB.status = "0";
            lodgeBillReqDB.transactionId = "";
            lodgeBillReqDB.vendorType = req.vendorAPIType.ToString();

            lodgeBillReqDB.create();

                log.Info("lodgebill inserted in DB");

                NCHLPartnerResp resObj = new NCHLPartnerResp();
            string msg = NCHLRepo.processRequest(NCHLBaseURL, NCHLLodgeBillEP, objVendor_API_Requests, ref lodgeReq, ref resObj, lodgeBillReqDB.instructionId, lodgeBillReqDB, true);

                log.Info("lodgebill request sent to NCHL");

                if (msg.ToLower() == "e999")
                {
                    var cres2 = new CommonResponse();
                    cres2.status = false;
                    cres2.ReponseCode = 3;
                    cres2.Message = "Please try again later.";//msg;
                    cres2.Details = "Please try again later.";//msg;
                    cres2.responseMessage = "Please try again later."; //msg;
                    var response2 = Request.CreateResponse<CommonResponse>(HttpStatusCode.BadGateway, cres2);

                    return response2;
                }

                if (msg.ToLower() != "success")
            {
                var cres2 = new CommonResponse();
                cres2.status = false;
                cres2.ReponseCode = 3;
                cres2.Message = msg;
                cres2.responseMessage = msg;
                    cres2.Details = msg;
                    var response2 = Request.CreateResponse<CommonResponse>(HttpStatusCode.BadGateway, cres2);

                return response2;
            }

            if (resObj.responseResult.responseCode != "000")

            {
                var cres2 = new CommonResponse();
                cres2.status = false;
                cres2.ReponseCode = 3;
                cres2.Message = resObj.responseResult.responseCode;
                cres2.responseMessage = resObj.responseResult.responseCode;
                    cres2.Details = resObj.responseResult.responseCode;
                    var response2 = Request.CreateResponse<CommonResponse>(HttpStatusCode.BadGateway, cres2);

                return response2;
            }


            lodgeBillReqDB.update(lodgeReq.cipsTransactionDetail.instructionId, resObj.responseResult.responseCode, JsonConvert.SerializeObject(lodgeReq), JsonConvert.SerializeObject(resObj), "", "");



            NCHLConsumerResp consumerResp = convertNPSLodgeToConsumerResp(resObj, VendorApiType);
            consumerResp.instructionId = lodgeReq.cipsTransactionDetail.instructionId;
            consumerResp.status = true;
            consumerResp.reponseCode = 1;

            response = Request.CreateResponse<NCHLConsumerResp>(HttpStatusCode.OK, consumerResp);
                consumerResp.serviceCharge = 0;

                return response;
            }
            catch (Exception ex)
            {
                log.Info("SubmitNCHLRequest caught exception: " + ex.ToString());

                //   throw;
            }
            return response;

        }


        [HttpPost]
        [Route("api/NCHLConfirmPayment")]

        public HttpResponseMessage confirmPayment(NCHLConsumerConfirmPayment userReq, string forwardedUserInput = "", bool validationAlreadyDone = false, bool isCreditCard= false, decimal serviceCharge = 0, string bank = "")
        {
            //if()

            

            string UserInput;

            if (forwardedUserInput == "")
            {
                UserInput = getRawPostData().Result;
            }
            else
            { 
                UserInput = forwardedUserInput;
            }


            var response = Request.CreateResponse<String>(System.Net.HttpStatusCode.OK, "Hello");

            if (!validationAlreadyDone)
            {
                var validationResponse = performGenericValidation(userReq, UserInput, true);

                if (validationResponse.StatusCode != HttpStatusCode.OK)
                {
                    return validationResponse;
                }
            }
            

            var lodgeBillReqDB = new LodgeBill();
            LodgeBill lodgeBill = lodgeBillReqDB.getRecord(userReq.instructionId);

            
            string NCHLLodgeBillEP = "api/billpayment/confirmbillpay.do";
            string NCHLURL = NCHLBaseURL + NCHLLodgeBillEP;
            string ApiResponse = string.Empty;

            //AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
            //int VendorApiType = userReq.vendorAPIType;

            //objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse(NCHLURL, userReq.UniqueCustomerId,Int64.Parse( userReq.MemberId), "", "", "", UserInput, userReq.DeviceCode, userReq.PlatForm, VendorApiType, "", "", "", ((int)VendorApi_CommonHelper.VendorTypes.MyPay).ToString());


            var lodgeBillReq = JsonConvert.DeserializeObject<NCHLLodgeReq>(lodgeBill.lodgeRequest);
            var lodgeBillResp = JsonConvert.DeserializeObject <NCHLPartnerResp>(lodgeBill.lodgeResponse);

            NCHLPartnerResp resObj = new NCHLPartnerResp();
            // string msg = NCHLRepo.processRequest(NCHLBaseURL, NCHLLodgeBillEP, objVendor_API_Requests, ref lodgeReq, ref resObj, lodgeBillReqDB.instructionId);
            string msg = "";

             //Get User 
             CommonResponse cres = new CommonResponse();
            string CommonResult = "";
            AddUserLoginWithPin resuser = new AddUserLoginWithPin();
            AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
            int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.Credit_Card_Payment;


            //Check Token

            string result = "";
            //string result = new VerificationHelper().getUser(HttpContext.Current.Request.Headers.GetValues("UToken").First(), req.DeviceId, ref user);
            //if (result != "success")
            //{
            //    return createErrorRespWithMessage(Request, result);
            //}

            VerificationHelper verificationHelper = new VerificationHelper();

            //Check PIN
            result = verificationHelper.checkPin(userReq.Mpin, resuser);
            if (result != "success")
            {
                return createErrorRespWithMessage(Request, result);
            }

            ///////////////////////////
            //Verify user PIN
            ///////////////////////////


            if (result != "success")
            {
                return createErrorRespWithMessage(Request, result);
            }



           
            

            
            decimal WalletBalance = Convert.ToDecimal(resuser.TotalAmount);

            //Wallet transaction
            decimal netAmount = ((decimal)lodgeBillResp.cipsTransactionDetail.amount); //+ ((decimal)lodgeBillResp.cipsTransactionDetail.chargeAmount);
            AddCalculateServiceChargeAndCashback objOut = Common.CalculateNetAmountWithServiceCharge(userReq.MemberId.ToString(), lodgeBillResp.cipsTransactionDetail.amount.ToString(), userReq.vendorAPIType.ToString());

            decimal totalamount = netAmount + objOut.ServiceCharge;

            if (!string.IsNullOrEmpty(userReq.MemberId.ToString()) && userReq.MemberId.ToString() != "0")
            {
                Int64 memId = Convert.ToInt64(userReq.MemberId);
                int Type = Convert.ToInt32(userReq.PaymentMode);
                resuser = new CommonHelpers().CheckUserDetail(userReq.UniqueCustomerId, UserInput, "", userReq.BankTransactionID, ref resGetCouponsScratched, "", userReq.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, true, totalamount.ToString(), true, userReq.Mpin);
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

            AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
            int VendorApiType = userReq.vendorAPIType;

            objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse(NCHLURL, userReq.UniqueCustomerId, Int64.Parse(userReq.MemberId), "", "", "", UserInput, userReq.DeviceCode, userReq.PlatForm, VendorApiType, "", "", "", ((int)VendorApi_CommonHelper.VendorTypes.MyPay).ToString());




            if (resuser.TotalAmount < totalamount)
            {
                cres = CommonApiMethod.ReturnBadRequestMessage("Insufficient Balance");
                response.StatusCode = HttpStatusCode.BadRequest;
                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                return response;
            }

            if (userReq.paymentMethodId == Convert.ToString((int)WalletTransactions.WalletTypes.MPCoins) && (Convert.ToDecimal(resuser .TotalRewardPoints) < objOut.MPCoinsDebit))
            {
                msg = Common.InsufficientBalance_MPCoins;
            }
            else if (userReq.paymentMethodId == Convert.ToString((int)WalletTransactions.WalletTypes.MPCoins) && (WalletBalance < (Convert.ToDecimal(objOut.NetAmount) - objOut.MPCoinsDebit)))
            {
                msg = Common.InsufficientBalance;
            }
            else if (userReq.paymentMethodId == "0" || userReq.paymentMethodId == Convert.ToString((int)WalletTransactions.WalletTypes.Wallet) && WalletBalance < Convert.ToDecimal(objOut.NetAmount))
            {
                msg = Common.InsufficientBalance;

            }

            string JsonReq = "";

            if (resuser == null || resuser.Id == 0)
            {
                msg = "MemberId not found";
                JsonReq = String.Empty;
            }
            //else if (resuser.IsKYCApproved != (int)AddUser.kyc.Verified)
            //{
            //    msg = Common.GetKycMessage(resuser, Convert.ToDecimal(userReq.Amount));
            //    if (!string.IsNullOrEmpty(msg))
            //    {
            //        JsonReq = String.Empty;
            //    }
            //}
            if (string.IsNullOrEmpty(msg))
            {
                if (resuser.IsActive == false)
                {
                    msg = "Your account is not active.";
                    JsonReq = String.Empty;
                }
            }

            string TransactionID = "";
            AddCouponsScratched coupon = new AddCouponsScratched();
            string VendorApiTypeName = @Enum.GetName(typeof(VendorApi_CommonHelper.KhaltiAPIName), Convert.ToInt64(VendorApiType)).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
            //objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse(Reference, resGetRecord.MemberId, MemberName, JsonReq, authenticationToken, UserInput, DeviceCode, PlatForm, VendorApiType);
            string TransactionUniqueId = "";

            if (isCreditCard)
            {
                var cardNo = lodgeBillReq.cipsTransactionDetail.refId;
              //  var bank = lodgeBillReq.cipsTransactionDetail.appId;
                TransactionUniqueId = VendorApi_CommonHelper.UpdateWalletBalance(coupon, ref TransactionID, userReq.BankTransactionID, userReq.PaymentMode, userReq.UniqueCustomerId, netAmount.ToString(), out msg, VendorApiType, resuser, objVendor_API_Requests, "", out WalletBalance, "", "", (int)VendorApi_CommonHelper.VendorTypes.NCHL,null, cardNo, bank);

                //string TransactionId = new CommonHelpers().GenerateUniqueId();
                //string Req_ReferenceNo = TransactionId;
                //string transactionresult = WalletDeduct(resGetCouponsScratched, objOut, resuser, totalamount, "", TransactionId, userReq.Amount, userReq.serviceCharge, userReq.r, userReq., resVendor_API_Requests, userReq.PlatForm, userReq.DeviceCode, VendorAPIType, ref walletUniqueTransactionId, Convert.ToInt32(userReq.PaymentMode)));
            }
            else
            {
                TransactionUniqueId = VendorApi_CommonHelper.UpdateWalletBalance(coupon, ref TransactionID, userReq.BankTransactionID, userReq.PaymentMode, userReq.UniqueCustomerId, netAmount.ToString(), out msg, VendorApiType, resuser, objVendor_API_Requests, "", out WalletBalance, "", "", (int)VendorApi_CommonHelper.VendorTypes.NCHL);

            }

            string authenticationToken = Request.Headers.Authorization.Parameter;

             msg = NCHLRepo.processRequest(NCHLBaseURL, NCHLLodgeBillEP, objVendor_API_Requests, ref lodgeBillReq, ref resObj, lodgeBillReqDB.instructionId, lodgeBill, false);

            var service = (VendorApi_CommonHelper.KhaltiAPIName)userReq.vendorAPIType;
            var serviceName = service.ToString().Replace("_", " ");

            bool IsCouponUnlocked = false;
            if (msg.ToLower() == "success" && objVendor_API_Requests.Id != 0 )//&& objVendor_API_Requests.Res_Khalti_State.ToLower() == "success")
            {
                string overRiddenDescription = "Payment successful for " + resObj.cipsTransactionDetail.appTxnId;
                objVendor_API_Requests.Res_Khalti_Status = true;
                msg = Common.UpdateCompleteTransaction(ref IsCouponUnlocked, ref TransactionID, resuser, objVendor_API_Requests, TransactionUniqueId, VendorApiTypeName, (isCreditCard ? lodgeBillReq.cipsTransactionDetail.refId : resObj.cipsTransactionDetail.appTxnId) ,"","",0,"", overRiddenDescription, resObj.cipsTransactionDetail.id.ToString(), resObj.cipsBatchDetail.batchId);
                if (msg.ToLower() == "success")
                {
                    string Title = "Transaction successfull";
                    string Message = $"Bill payment of amount Rs.{totalamount} for {VendorApiTypeName} has been completed successfully.";//TransactionId " + resKhalti.TransactionUniqueId + " success for " + VendorApiTypeName;
                    Common.SendNotification(authenticationToken, VendorApiType, resuser.MemberId, Title, Message);

                    if (isCreditCard)
                    {
                        WalletTransactions res_transaction = new WalletTransactions();
                         res_transaction.TransactionUniqueId = TransactionUniqueId;
                        if (res_transaction.GetRecord())
                        {
                            string mystring = File.ReadAllText(HttpContext.Current.Server.MapPath("/Templates/CreditCardPayment.html"));
                            string body = mystring;
                            body = body.Replace("##Amount##", res_transaction.Amount.ToString("0.00"));
                            body = body.Replace("##TransactionId##", res_transaction.TransactionUniqueId);
                            body = body.Replace("##ConsumerTransactionId##", res_transaction.Reference);
                            body = body.Replace("##Date##", Common.fnGetdatetimeFromInput(res_transaction.CreatedDate));
                            body = body.Replace("##Type##", WalletTransactions.Signs.Debit.ToString());
                            body = body.Replace("##Service##", ((VendorApi_CommonHelper.KhaltiAPIName)Convert.ToInt32(res_transaction.Type)).ToString().Replace("khalti", " ").Replace("_", " "));
                            body = body.Replace("##Status##", WalletTransactions.Statuses.Success.ToString());
                            body = body.Replace("##Cashback##", res_transaction.CashBack.ToString("0.00"));
                            body = body.Replace("##ServiceCharge##", res_transaction.ServiceCharge.ToString("0.00"));
                            body = body.Replace("##TotalAmount##", res_transaction.NetAmount.ToString("0.00"));
                            body = body.Replace("##Remarks##", res_transaction.Remarks.ToString().Replace("<", " "));
                            body = body.Replace("##From##", res_transaction.MemberName);
                            body = body.Replace("##To##", res_transaction.RecieverName);

                            string Subject = MyPay.Models.Common.Common.WebsiteName + " - Credit card Payment Successfull";
                            if (!string.IsNullOrEmpty(resuser.Email))
                            {
                                body = body.Replace("##UserName##", resuser.FirstName);
                                MyPay.Models.Common.Common.SendAsyncMail(resuser.Email, Subject, body);
                            }
                      
                        }

                    }

                    //GET SERVICE NAME
                    string localServiceName = getLocalServiceForNCHLGovtService(resObj.cipsTransactionDetail.appId, isCreditCard);

                    var list = new List<KeyValuePair<String, String>>();

                    VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Date", DateTime.Now.ToString());
                    VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Type", "Government Payment");
                    VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Service", localServiceName);
                    VendorApi_CommonHelper.addKeyValueToList(ref list, "Voucher Number", resObj.cipsTransactionDetail.appTxnId);
                    VendorApi_CommonHelper.addKeyValueToList(ref list, "Customer Name", resObj.cipsTransactionDetail.particulars);
                    VendorApi_CommonHelper.addKeyValueToList(ref list, "Transaction Status", "Success");
                    VendorApi_CommonHelper.addKeyValueToList(ref list, "Service Charge(RED)", objOut.ServiceCharge.ToString());
                    VendorApi_CommonHelper.addKeyValueToList(ref list, "Paid(RED)", totalamount.ToString());
                    VendorApi_CommonHelper.addKeyValueToList(ref list, "Remarks", localServiceName + " payment of Rs. " + netAmount + " paid successfully for " + resObj.cipsTransactionDetail.appTxnId + ".");

                    string JSONForReceipt = VendorApi_CommonHelper.getJSONfromList(list);

                    VendorApi_CommonHelper.saveReceipt(userReq.vendorAPIType.ToString(), serviceName, userReq.MemberId, TransactionUniqueId, JSONForReceipt, resuser.ContactNumber, resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName, serviceName , resObj.cipsTransactionDetail.appTxnId , netAmount.ToString());
                }
               
            }
            else if (msg.ToLower() == "e777")
            {

            }
            else
            {
                
                if (isCreditCard)
                {
                    WalletRefund(TransactionUniqueId, resuser, netAmount, resuser.PlatForm, resuser.DeviceCode);
                }
                else {
                    objVendor_API_Requests.Res_Khalti_State = "failed";
                    Common.RefundUpdateTransaction(resuser, objVendor_API_Requests, TransactionUniqueId, VendorApiTypeName, userReq.BankTransactionID, VendorApiType, userReq.paymentMethodId, resuser.PlatForm, resuser.DeviceCode);

                }

            }

            //Wallet transaction ends here

            if (msg.ToLower() == "e999")
            {
                var cres2 = new CommonResponse();
                cres2.status = false;
                cres2.ReponseCode = 3;
                cres2.Message = "Please try again later." ;//msg;
                cres2.responseMessage = "Please try again later."; //msg;
                cres2.Details = "Please try again later."; //msg;
                var response2 = Request.CreateResponse<CommonResponse>(HttpStatusCode.BadGateway, cres2);

                // Common.RefundUpdateTransaction(resuser, objVendor_API_Requests, TransactionUniqueId, VendorApiTypeName, userReq.BankTransactionID, VendorApiType, userReq.paymentMethodId, resuser.PlatForm, resuser.DeviceCode);
                //if (isCreditCard)
                //{
                //    WalletRefund(TransactionUniqueId, resuser, totalamount, resuser.PlatForm, resuser.DeviceCode);
                //}
                //else
                //{
                //    objVendor_API_Requests.Res_Khalti_State = "failed";
                //    Common.RefundUpdateTransaction(resuser, objVendor_API_Requests, TransactionUniqueId, VendorApiTypeName, userReq.BankTransactionID, VendorApiType, userReq.paymentMethodId, resuser.PlatForm, resuser.DeviceCode);

                //}

                return response2;
            }

            if (msg.ToLower() != "success")
            {
                var cres2 = new CommonResponse();
                cres2.status = false;
                cres2.ReponseCode = 3;
                cres2.Message = msg;
                cres2.responseMessage = msg;
                cres2.Details = msg;
                var response2 = Request.CreateResponse<CommonResponse>(HttpStatusCode.BadGateway, cres2);

                // Common.RefundUpdateTransaction(resuser, objVendor_API_Requests, TransactionUniqueId, VendorApiTypeName, userReq.BankTransactionID, VendorApiType, userReq.paymentMethodId, resuser.PlatForm, resuser.DeviceCode);

                //if (isCreditCard)
                //{
                //    WalletRefund(TransactionUniqueId, resuser, totalamount, resuser.PlatForm, resuser.DeviceCode);
                //}
                //else
                //{
                //    objVendor_API_Requests.Res_Khalti_State = "failed";
                //    Common.RefundUpdateTransaction(resuser, objVendor_API_Requests, TransactionUniqueId, VendorApiTypeName, userReq.BankTransactionID, VendorApiType, userReq.paymentMethodId, resuser.PlatForm, resuser.DeviceCode);

                //}
                return response2;
            }
            lodgeBill.update( lodgeBillReq.cipsTransactionDetail.instructionId , resObj.responseResult.responseCode , "", "", JsonConvert.SerializeObject(lodgeBillReq), JsonConvert.SerializeObject(resObj));

           

            NCHLConsumerResp consumerResp = convertNPSLodgeToConsumerResp(resObj, VendorApiType);
            consumerResp.status = true;
            consumerResp.reponseCode = 1;
            consumerResp.message = "Success";
            consumerResp.TransactionUniqueId = TransactionUniqueId;
            consumerResp.Description = "Payment to " + serviceName + " successful.";

            response = Request.CreateResponse<NCHLConsumerResp>(HttpStatusCode.OK, consumerResp);

            return response;
        }



        public NCHLConsumerResp convertNPSLodgeToConsumerResp(NCHLPartnerResp partnerRespObj, int vendorAPIType) {


            NCHLConsumerResp response = new NCHLConsumerResp();

            var service = respMapper.services.Find(x => x.vendorAPIType == vendorAPIType);

            //Use mapper to map the request
            var mapperType = service.mapping.GetType();
            IList<PropertyInfo> mapperProperties = mapperType.GetProperties();
            var mapperPropertiesList = mapperProperties.Cast<PropertyInfo>().ToList();

            var partnerRespType = partnerRespObj.cipsTransactionDetail.GetType();
            IList<PropertyInfo> reqProperties = partnerRespType.GetProperties();
            var partnerRespPropertiesList = reqProperties.Cast<PropertyInfo>().ToList();

             var consumerRespType = response.GetType();
            IList<PropertyInfo> consumerRespProperties = consumerRespType.GetProperties();
            var consumerRespPropertiesList = consumerRespProperties.Cast<PropertyInfo>().ToList();

            try
            {
                foreach (PropertyInfo property in mapperProperties)
                {

                    if (property.Name != "vendorAPIType" && property.Name != "applicationID" && property.Name != "serviceName" && property.GetValue(service.mapping) != null)
                    {
                        foreach (var consumerProperty in consumerRespPropertiesList)
                        {
                            if (consumerProperty.Name == property.Name)
                            {
                                foreach (var partnerProperty in partnerRespPropertiesList)
                                {
                                    if (partnerProperty.Name == property.GetValue(service.mapping).ToString())
                                    {
                                        consumerProperty.SetValue(response, partnerProperty.GetValue(partnerRespObj.cipsTransactionDetail));
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                //throw;
            }
            
            return response;
        }


        public NCHLLodgeReq convertToNPSLodgeReq(NCHLConsumerLodgeReq req, int vendorAPIType)
        {
            NCHLLodgeReq lodgeReq = new NCHLLodgeReq();

            var service = requestMapper.services.Find(x => x.vendorAPIType == vendorAPIType);

            lodgeReq.cipsBatchDetail = JsonConvert.DeserializeObject<Models.NPS.CipsBatchDetail>("{\"batchId\": \"MP10000123222200022\",\"batchAmount\": 627.5,\"batchCount\": 1,\"batchCrncy\": \"NPR\",\"categoryPurpose\": \"ECPG\",\"debtorAgent\": \"2501\",\"debtorBranch\": \"1\",\"debtorName\": \"MONA SHRESTHA\",\"debtorAccount\": \"0010000055900011\"}");
            var batchID =  DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() + req.MemberId;

            lodgeReq.cipsBatchDetail.batchCount = 1;
            lodgeReq.cipsBatchDetail.batchAmount = req.amount;
            lodgeReq.cipsBatchDetail.batchId = batchID;
            lodgeReq.cipsTransactionDetail.endToEndId = batchID;
            lodgeReq.cipsTransactionDetail.instructionId = batchID;


            if (Common.ApplicationEnvironment.IsProduction) {
                lodgeReq.cipsBatchDetail.debtorAccount = Common.ConnectIPs_AccountNumber;
                lodgeReq.cipsBatchDetail.debtorAgent = Common.ConnectIps_BankId;
                lodgeReq.cipsBatchDetail.debtorBranch = Common.ConnectIPs_BranchId;
                lodgeReq.cipsBatchDetail.debtorName = Common.ConnectIps_AccountName;
            }

            //Use mapper to map the request
            var mapperType = service.mapping.GetType();
            IList<PropertyInfo> mapperProperties = mapperType.GetProperties();
            var mapperPropertiesList = mapperProperties.Cast<PropertyInfo>().ToList();

            var reqType = req.GetType();
            IList<PropertyInfo> reqProperties = reqType.GetProperties();
            var reqPropertiesList = reqProperties.Cast<PropertyInfo>().ToList();

            var npsLodgeType = lodgeReq.cipsTransactionDetail.GetType();
            IList<PropertyInfo> npsLodgeTxnProperties = npsLodgeType.GetProperties();
            var npsLodgeTxnPropertiesList = npsLodgeTxnProperties.Cast<PropertyInfo>().ToList();

            foreach (PropertyInfo property in mapperProperties)
            {
                if (property.Name != "applicationID" && property.Name != "serviceName" && property.GetValue(service.mapping) != null)
                {
                    npsLodgeTxnPropertiesList.Find(x => x.Name == property.Name).SetValue(lodgeReq.cipsTransactionDetail,
                        (reqPropertiesList.Find(x => x.Name == property.GetValue(service.mapping).ToString()).GetValue(req))
                        );
                }
            }

            if (vendorAPIType == 80)
            {
             //   lodgeReq.cipsTransactionDetail.appId = lodgeReq.cipsTransactionDetail.refId;
            }
            else
            {
                lodgeReq.cipsTransactionDetail.appId = service.mapping.applicationID;
            }

            return lodgeReq;
        }

        public string getLocalServiceForNCHLGovtService(String appID, bool isCreditCard = false)
        {

            if (isCreditCard)
            {
                return "Credit card";
            }

            string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();

            using (var connection = new SqlConnection(connectionString))
            {

                NCHLServiceMappingDB serviceData = new NCHLServiceMappingDB();
                var storedProcedureName = "sp_getNCHLGovtServiceName";
                var values = new
                {
                    NCHLServiceName = appID,
                };
                try
                {
                    serviceData = connection.QueryFirstOrDefault<NCHLServiceMappingDB>(storedProcedureName, values, commandType: CommandType.StoredProcedure);
                }
                catch (SqlException sqlEx)
                {

                }

                return serviceData.LocalServiceName;
            }
        }

        //CREDIT CARD


        [HttpPost]
        [Route("api/GetCreditCardIsuuerLists")]

        public HttpResponseMessage GetCreditCardIsuuerLists()
        {
            CCIssuerListResp.Message = "success";
            CCIssuerListResp.status = true;
            CCIssuerListResp.ReponseCode = 1;
            CCIssuerListResp.responseMessage = "success";
            var response = Request.CreateResponse<Res_CreditCardIsuuerList>(HttpStatusCode.OK, CCIssuerListResp);
            return response;
        }


        [HttpPost]
        [Route("api/CreditCardPayment")]

        public HttpResponseMessage CreditCardPayment(Req_CreditCardPayment userReq)
        {

            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            log.Info("Inside NCHL lodge request");

            var response = Request.CreateResponse<String>(System.Net.HttpStatusCode.InternalServerError, "Couldnt process request");

            try
            {


                string UserInput = getRawPostData().Result;

                NCHLConsumerLodgeReq req = new NCHLConsumerLodgeReq();
                req.Amount = Decimal.ToDouble(userReq.Amount); 
                req.amount = Decimal.ToDouble(userReq.Amount);
                req.Name = userReq.Name;
                req.CardNumber = userReq.CardNumber;
                req.DeviceCode = userReq.DeviceCode;
                req.DeviceId = userReq.DeviceId;
                req.Code = userReq.Code;
                req.Version = userReq.Version;
                req.PlatForm = userReq.PlatForm;
                req.TimeStamp = userReq.TimeStamp;
                req.Token = userReq.Token;
                req.UniqueCustomerId = userReq.UniqueCustomerId;
                req.SecretKey = userReq.SecretKey;
                req.Mpin = userReq.Mpin;
                req.Hash = userReq.Hash;
                req.vendorAPIType = 80;
                req.MemberId = userReq.MemberId.ToString();
                req.paymentType = userReq.PaymentMode;


                var validationResponse = performGenericValidation(req, UserInput, true);

                if (validationResponse.StatusCode != HttpStatusCode.OK)
                {
                    return validationResponse;
                }

                log.Info("NCHL lodge request generic validation passed");

                string NCHLLodgeBillEP = "api/billpayment/lodgebillpay.do";
                string NCHLURL = NCHLBaseURL + NCHLLodgeBillEP;
                string ApiResponse = string.Empty;

                AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                int VendorApiType = req.vendorAPIType;
                objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse(NCHLURL, req.UniqueCustomerId, Common.CreatedBy, Common.CreatedByName, "", "", UserInput, req.DeviceCode, req.PlatForm, VendorApiType, "", "", "", ((int)VendorApi_CommonHelper.VendorTypes.MyPay).ToString());


                var lodgeReq = convertToNPSLodgeReq(req, VendorApiType);
                lodgeReq.generateToken();

                log.Info("token generation ended");

                var lodgeBillReqDB = new LodgeBill();
                lodgeBillReqDB.batchId = lodgeReq.cipsBatchDetail.batchId;
                lodgeBillReqDB.instructionId = lodgeReq.cipsTransactionDetail.instructionId;
                lodgeBillReqDB.endToEndId = lodgeReq.cipsTransactionDetail.endToEndId;
                lodgeBillReqDB.amount = lodgeReq.cipsTransactionDetail.amount.ToString();
                lodgeBillReqDB.appId = lodgeReq.cipsTransactionDetail.appId;
                lodgeBillReqDB.confirmRequest = "";
                lodgeBillReqDB.confirmResponse = "";
                lodgeBillReqDB.lodgeRequest = JsonConvert.SerializeObject(lodgeReq);
                //lodgeBillReqDB.lodgeResponse = JsonConvert.SerializeObject(resObj);
                lodgeBillReqDB.memberId = req.MemberId;
                lodgeBillReqDB.status = "0";
                lodgeBillReqDB.transactionId = "";
                lodgeBillReqDB.vendorType = req.vendorAPIType.ToString();

                lodgeBillReqDB.create();

                log.Info("lodgebill inserted in DB");

                NCHLPartnerResp resObj = new NCHLPartnerResp();
                string msg = NCHLRepo.processRequest(NCHLBaseURL, NCHLLodgeBillEP, objVendor_API_Requests, ref lodgeReq, ref resObj, lodgeBillReqDB.instructionId, lodgeBillReqDB, true);

                log.Info("lodgebill request sent to NCHL");

                if (msg.ToLower() == "e999")
                {
                    var cres2 = new CommonResponse();
                    cres2.status = false;
                    cres2.ReponseCode = 3;
                    cres2.Message = "Please try again later.";//msg;
                    cres2.responseMessage = "Please try again later."; //msg;
                    cres2.Details = "Please try again later."; //msg;
                    var response2 = Request.CreateResponse<CommonResponse>(HttpStatusCode.BadGateway, cres2);

                    return response2;
                }

                if (msg.ToLower() != "success")
                {
                    var cres2 = new CommonResponse();
                    cres2.status = false;
                    cres2.ReponseCode = 3;
                    cres2.Message = msg;
                    cres2.responseMessage = msg;
                    cres2.Details = msg;
                    var response2 = Request.CreateResponse<CommonResponse>(HttpStatusCode.BadGateway, cres2);

                    return response2;
                }

                if (resObj.responseResult.responseCode != "000")

                {
                    var cres2 = new CommonResponse();
                    cres2.status = false;
                    cres2.ReponseCode = 3;
                    cres2.Message = resObj.responseResult.responseCode;
                    cres2.Details = resObj.responseResult.responseCode;
                    cres2.responseMessage = resObj.responseResult.responseCode;
                    var response2 = Request.CreateResponse<CommonResponse>(HttpStatusCode.BadGateway, cres2);

                    return response2;
                }


                lodgeBillReqDB.update(lodgeReq.cipsTransactionDetail.instructionId, resObj.responseResult.responseCode, JsonConvert.SerializeObject(lodgeReq), JsonConvert.SerializeObject(resObj), "", "");


               


                NCHLConsumerResp consumerResp = convertNPSLodgeToConsumerResp(resObj, VendorApiType);
                consumerResp.instructionId = lodgeReq.cipsTransactionDetail.instructionId;
                consumerResp.status = true;
                consumerResp.reponseCode = 1;

                NCHLConsumerConfirmPayment paymentRequest = new NCHLConsumerConfirmPayment();
                paymentRequest = JsonConvert.DeserializeObject<NCHLConsumerConfirmPayment>(JsonConvert.SerializeObject(req));
                paymentRequest.instructionId = consumerResp.instructionId;
                paymentRequest.PaymentMode = userReq.PaymentMode;

                //HttpResponseMessage
                UserInput = UserInput.Replace("}", ", \"serviceReqPart\": 2}");
                HttpResponseMessage resp = confirmPayment(paymentRequest, UserInput, true, true, userReq.ServiceCharge, userReq.BankName);
                return resp;
                //if (resp.StatusCode == HttpStatusCode.OK)
                //{
                //    return resp;
                //    //var genericResponse = new CommonResponse();
                //    //genericResponse.responseMessage = "Credit Card Bill Payment Successful.";
                //    //genericResponse.Message = "success";
                //    //genericResponse.Details = "Credit card Payment Successfull";
                //    //genericResponse.ReponseCode = 1;
                //    //genericResponse.status = true;
                //    //genericResponse.TransactionUniqueId = resp.Content
                //    //var finalResponse = Request.CreateResponse<CommonResponse>(HttpStatusCode.OK, genericResponse);
                //    //return finalResponse;
                //}
                //else
                //{
                //    return resp;
                //}
                
                //var finalResponse = requestMapper
                //response = Request.CreateResponse<NCHLConsumerResp>(HttpStatusCode.OK, consumerResp);
                //consumerResp.serviceCharge = 0;

                return response;
            }
            catch (Exception ex)
            {
                log.Info("SubmitNCHLRequest caught exception: " + ex.ToString());
                //   throw;
            }
            return response;

        }

        public static string WalletDeduct(AddCouponsScratched resCoupon, AddCalculateServiceChargeAndCashback objOut, AddUserLoginWithPin resuser, decimal totalamount, string authenticationtoken, string TransactionId, decimal amount, decimal servicecharge, string cardnumber, string code, AddVendor_API_Requests resVendor_API_Requests, string PlatForm, string DeviceCode, Int32 VendorApiType, ref string uniqueTransactionId, Int32 WalletType = 1)
        {
            string result = "";
            try
            {
                decimal CouponDeduct = 0;
                decimal WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resuser.TotalAmount) - Convert.ToDecimal(totalamount));
                decimal RewardPointBalance = Convert.ToDecimal(resuser.TotalRewardPoints);
                if (WalletType == 0 || WalletType == ((int)WalletTransactions.WalletTypes.Wallet))
                {
                    WalletType = ((int)WalletTransactions.WalletTypes.Wallet);
                    CouponDeduct = CouponDeduct + Convert.ToDecimal((Convert.ToDecimal(amount) * resCoupon.CouponPercentage) / 100);
                    WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resuser.TotalAmount) - Convert.ToDecimal(totalamount) - CouponDeduct);
                    RewardPointBalance = Convert.ToDecimal(resuser.TotalRewardPoints);
                }
                else if (WalletType == ((int)WalletTransactions.WalletTypes.MPCoins))
                {
                    CouponDeduct = CouponDeduct + Convert.ToDecimal((Convert.ToDecimal(amount) * resCoupon.CouponPercentage) / 100);
                    WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resuser.TotalAmount) - ((Convert.ToDecimal(totalamount) - objOut.MPCoinsDebit - CouponDeduct)));
                    RewardPointBalance = Convert.ToDecimal(Convert.ToDecimal(resuser.TotalRewardPoints) - objOut.MPCoinsDebit);
                }
                WalletTransactions res_transaction = new WalletTransactions();
                res_transaction.VendorTransactionId = TransactionId;
                if (!res_transaction.GetRecordCheckExists())
                {
                    res_transaction.MemberId = Convert.ToInt64(resuser.MemberId);
                    res_transaction.ContactNumber = resuser.ContactNumber;
                    res_transaction.MemberName = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
                    if (WalletType == ((int)WalletTransactions.WalletTypes.MPCoins))
                    {
                        res_transaction.Amount = Convert.ToDecimal((amount + servicecharge)) - objOut.MPCoinsDebit;
                    }
                    else
                    {
                        res_transaction.Amount = Convert.ToDecimal((amount + servicecharge)) - Convert.ToDecimal(CouponDeduct);
                    }
                    res_transaction.UpdateBy = Convert.ToInt64(resuser.MemberId);
                    res_transaction.UpdateByName = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
                    res_transaction.CurrentBalance = WalletBalance;
                    res_transaction.RewardPointBalance = RewardPointBalance;
                    res_transaction.CreatedBy = Common.GetCreatedById(authenticationtoken);
                    res_transaction.CreatedByName = Common.GetCreatedByName(authenticationtoken);
                    res_transaction.TransactionUniqueId = new CommonHelpers().GenerateUniqueId();
                    uniqueTransactionId = res_transaction.TransactionUniqueId;
                    res_transaction.VendorTransactionId = new CommonHelpers().GenerateUniqueId();
                    res_transaction.Reference = TransactionId;
                    //res_transaction.BatchTransactionId = res.cipsBatchResponse.batchId;
                    //res_transaction.TxnInstructionId = res.cipsTxnResponseList[0].instructionId;
                    res_transaction.Remarks = "Credit Card Payment Successfully Deposit";
                    res_transaction.GatewayStatus = WalletTransactions.Statuses.Pending.ToString();
                    res_transaction.ResponseCode = "";
                    res_transaction.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Credit_Card_Payment;
                    res_transaction.Description = "Credit Card Payment Successfully Deposit";
                    res_transaction.Status = (int)WalletTransactions.Statuses.Pending;
                    res_transaction.IsApprovedByAdmin = true;
                    res_transaction.IsActive = true;
                    res_transaction.Sign = Convert.ToInt16(WalletTransactions.Signs.Debit);
                    res_transaction.RecieverName = Common.ConnectIPs_AccountNumber;
                    res_transaction.TransferType = (int)WalletTransactions.TransferTypes.Sender;
                    res_transaction.RecieverAccountNo = Common.ConnectIPs_AccountNumber;
                    res_transaction.RecieverBankCode = Common.ConnectIps_BankId;
                    res_transaction.RecieverBranch = Common.ConnectIPs_BranchName;
                    res_transaction.CardNumber = cardnumber;
                    res_transaction.CardType = code;
                    res_transaction.ServiceCharge = (objOut.ServiceCharge + servicecharge);
                    res_transaction.NetAmount = totalamount;
                    res_transaction.Purpose = "Credit Card Payment";
                    res_transaction.Platform = PlatForm;
                    res_transaction.WalletType = WalletType;
                    res_transaction.VendorType = (int)VendorApi_CommonHelper.VendorTypes.Prabhu;

                    res_transaction.RecieverBankName = Common.ConnectIPs_BankName;
                    res_transaction.RecieverBranchName = Common.ConnectIPs_BranchName;

                    if ((WalletType == ((int)WalletTransactions.WalletTypes.Wallet) || WalletType == ((int)WalletTransactions.WalletTypes.FonePay)))
                    {
                        res_transaction.TransactionAmount = objOut.Amount;
                        res_transaction.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
                        res_transaction.CouponCode = resCoupon.CouponCode;
                        res_transaction.CouponDiscount = CouponDeduct;
                    }
                    else if (WalletType == ((int)WalletTransactions.WalletTypes.MPCoins))
                    {
                        res_transaction.TransactionAmount = objOut.Amount;
                        res_transaction.MPCoinsDebit = objOut.MPCoinsDebit;
                        res_transaction.RewardPointBalance = (resuser.TotalRewardPoints) - objOut.MPCoinsDebit;
                        res_transaction.WalletType = (int)WalletTransactions.WalletTypes.MPCoins;
                        res_transaction.CouponDiscount = CouponDeduct;
                    }
                    if (res_transaction.Add())
                    {
                        Common.AddLogs("Credit Card Payment Successfully Deposit", false, Convert.ToInt32(AddLog.LogType.Credit_Card_Payment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, PlatForm, DeviceCode);
                        result = "success";
                    }
                    else
                    {
                        Common.AddLogs("Something Went Wrong Payment Not Sent", false, Convert.ToInt32(AddLog.LogType.Credit_Card_Payment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, PlatForm, DeviceCode);
                        result = "Something Went Wrong Payment Not Sent";
                    }
                }
                else
                {

                    Common.AddLogs("Transaction Sent Already", false, Convert.ToInt32(AddLog.LogType.Credit_Card_Payment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, PlatForm, DeviceCode);
                    result = "Transaction Sent Already";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        public static string WalletRefund(string TransactionId, AddUserLoginWithPin resuser, decimal totalamount, string Platform, string DeviceCode)
        {
            string result = "";
            try
            {
                WalletTransactions res_transaction = new WalletTransactions();
                res_transaction.TransactionUniqueId = TransactionId;
                if (res_transaction.GetRecord())
                {
                    WalletTransactions newtra = new WalletTransactions();
                    decimal WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resuser.TotalAmount));
                    newtra.MemberId = Convert.ToInt64(resuser.MemberId);
                    newtra.ContactNumber = resuser.ContactNumber;
                    newtra.MemberName = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
                    newtra.Amount = (res_transaction.NetAmount);
                    newtra.UpdateBy = Convert.ToInt64(resuser.MemberId);
                    newtra.UpdateByName = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
                    newtra.CurrentBalance = WalletBalance;
                    newtra.CreatedBy = 10000;//Common.GetCreatedById(authenticationtoken);
                    newtra.CreatedByName = "";//Common.GetCreatedByName(authenticationtoken);
                    newtra.TransactionUniqueId = new CommonHelpers().GenerateUniqueId();
                    newtra.VendorTransactionId = new CommonHelpers().GenerateUniqueId();
                    newtra.Reference = TransactionId;
                    newtra.ParentTransactionId = res_transaction.TransactionUniqueId;
                    newtra.Remarks = "Credit Card Payment Successfully Refunded for Transactino ID: " + res_transaction.TransactionUniqueId;
                    newtra.GatewayStatus = WalletTransactions.Statuses.Success.ToString();
                    newtra.ResponseCode = "";
                    newtra.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Credit_Card_Payment;
                    newtra.Description = "Credit Card Payment Successfully Refunded";
                    newtra.Status = (int)WalletTransactions.Statuses.Refund;
                    newtra.IsApprovedByAdmin = true;
                    newtra.IsActive = true;
                    newtra.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
                    newtra.RecieverName = Common.ConnectIPs_AccountNumber;
                    newtra.TransferType = (int)WalletTransactions.TransferTypes.Sender;
                    //newtra.RecieverAccountNo = Common.ConnectIPs_AccountNumber;
                    //newtra.RecieverBankCode = Common.ConnectIps_BankId;
                    //newtra.RecieverBranch = Common.ConnectIPs_BranchName;
                    newtra.CardNumber = res_transaction.CardNumber;
                    newtra.CardType = res_transaction.CardType;
                  //  newtra.ServiceCharge = (res_transaction.ServiceCharge);
                    newtra.NetAmount = totalamount;
                    newtra.Purpose = "Credit Card Payment Refund";
                    newtra.Platform = Platform;
                    newtra.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
                    newtra.VendorType = (int)VendorApi_CommonHelper.VendorTypes.MyPay;


                    newtra.Amount = Convert.ToDecimal(res_transaction.NetAmount);
                    newtra.RewardPoint = res_transaction.RewardPoint;
                    newtra.RewardPointBalance = res_transaction.RewardPointBalance;
                    if (res_transaction.WalletType == ((int)WalletTransactions.WalletTypes.MPCoins))
                    {
                        newtra.Amount = Convert.ToDecimal(res_transaction.Amount) + Convert.ToDecimal(res_transaction.ServiceCharge);
                        newtra.RewardPoint = res_transaction.RewardPoint;
                        newtra.MPCoinsDebit = res_transaction.MPCoinsDebit;
                        newtra.RewardPointBalance = res_transaction.RewardPointBalance;
                        WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resuser.TotalAmount));
                    }

                    if (newtra.Add())
                    {
                        res_transaction.Remarks = "Credit Card Payment Failed";
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
                        Common.AddLogs("Something Went Wrong Payment Not Sent", false, Convert.ToInt32(AddLog.LogType.Credit_Card_Payment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, Platform, DeviceCode);
                        result = "Something Went Wrong Payment Not Sent";
                    }
                }
                else
                {
                    result = "Transaction Not Found For Refund";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        //CREDIT CARD


        //[HttpPost]
        //[Route("api/GetCreditCardIsuuerLists")]

        //public HttpResponseMessage GetCreditCardIsuuerLists()
        //{
        //    CCIssuerListResp.Message = "success";
        //    CCIssuerListResp.status = true;
        //    CCIssuerListResp.ReponseCode = 1;
        //    CCIssuerListResp.responseMessage = "success";
        //    var response = Request.CreateResponse<Res_CreditCardIsuuerList>(HttpStatusCode.OK, CCIssuerListResp);
        //    return response;
        //}


        //[HttpPost]
        //[Route("api/CreditCardPayment")]

        //public HttpResponseMessage CreditCardPayment(Req_CreditCardPayment userReq)
        //{

        //    log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
        //    ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //    log.Info("Inside NCHL lodge request");

        //    var response = Request.CreateResponse<String>(System.Net.HttpStatusCode.InternalServerError, "Couldnt process request");

        //    try
        //    {


        //        string UserInput = getRawPostData().Result;

        //        NCHLConsumerLodgeReq req = new NCHLConsumerLodgeReq();
        //        req.Amount = userReq.Amount;
        //        req.Name = userReq.Name;
        //        req.CardNumber = userReq.CardNumber;
        //        req.DeviceCode = userReq.DeviceCode;
        //        req.DeviceId = userReq.DeviceId;
        //        req.Version = userReq.Version;
        //        req.PlatForm = userReq.PlatForm;
        //        req.TimeStamp = userReq.TimeStamp;
        //        req.Token = userReq.Token;
        //        req.UniqueCustomerId = userReq.UniqueCustomerId;
        //        req.SecretKey = userReq.SecretKey;
        //        req.Mpin = userReq.Mpin;
        //        req.Hash = userReq.Hash;
        //        req.MemberId = userReq.MemberId.ToString();


        //        var validationResponse = performGenericValidation(req, UserInput, true);

        //        if (validationResponse.StatusCode != HttpStatusCode.OK)
        //        {
        //            return validationResponse;
        //        }

        //        log.Info("NCHL lodge request generic validation passed");

        //        string NCHLLodgeBillEP = "api/billpayment/lodgebillpay.do";
        //        string NCHLURL = NCHLBaseURL + NCHLLodgeBillEP;
        //        string ApiResponse = string.Empty;

        //        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
        //        int VendorApiType = req.vendorAPIType;
        //        objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse(NCHLURL, req.UniqueCustomerId, Common.CreatedBy, Common.CreatedByName, "", "", UserInput, req.DeviceCode, req.PlatForm, VendorApiType, "", "", "", ((int)VendorApi_CommonHelper.VendorTypes.MyPay).ToString());


        //        var lodgeReq = convertToNPSLodgeReq(req, req.vendorAPIType);
        //        lodgeReq.generateToken();

        //        log.Info("token generation ended");

        //        var lodgeBillReqDB = new LodgeBill();
        //        lodgeBillReqDB.batchId = lodgeReq.cipsBatchDetail.batchId;
        //        lodgeBillReqDB.instructionId = lodgeReq.cipsTransactionDetail.instructionId;
        //        lodgeBillReqDB.endToEndId = lodgeReq.cipsTransactionDetail.endToEndId;
        //        lodgeBillReqDB.amount = lodgeReq.cipsTransactionDetail.amount.ToString();
        //        lodgeBillReqDB.appId = lodgeReq.cipsTransactionDetail.appId;
        //        lodgeBillReqDB.confirmRequest = "";
        //        lodgeBillReqDB.confirmResponse = "";
        //        lodgeBillReqDB.lodgeRequest = JsonConvert.SerializeObject(lodgeReq);
        //        //lodgeBillReqDB.lodgeResponse = JsonConvert.SerializeObject(resObj);
        //        lodgeBillReqDB.memberId = req.MemberId;
        //        lodgeBillReqDB.status = "0";
        //        lodgeBillReqDB.transactionId = "";
        //        lodgeBillReqDB.vendorType = req.vendorAPIType.ToString();

        //        lodgeBillReqDB.create();

        //        log.Info("lodgebill inserted in DB");

        //        NCHLPartnerResp resObj = new NCHLPartnerResp();
        //        string msg = NCHLRepo.processRequest(NCHLBaseURL, NCHLLodgeBillEP, objVendor_API_Requests, ref lodgeReq, ref resObj, lodgeBillReqDB.instructionId);

        //        log.Info("lodgebill request sent to NCHL");

        //        if (msg.ToLower() == "e999")
        //        {
        //            var cres2 = new CommonResponse();
        //            cres2.status = false;
        //            cres2.ReponseCode = 3;
        //            cres2.Message = "Please try again later.";//msg;
        //            cres2.responseMessage = "Please try again later."; //msg;
        //            var response2 = Request.CreateResponse<CommonResponse>(HttpStatusCode.BadGateway, cres2);

        //            return response2;
        //        }

        //        if (msg.ToLower() != "success")
        //        {
        //            var cres2 = new CommonResponse();
        //            cres2.status = false;
        //            cres2.ReponseCode = 3;
        //            cres2.Message = msg;
        //            cres2.responseMessage = msg;
        //            var response2 = Request.CreateResponse<CommonResponse>(HttpStatusCode.BadGateway, cres2);

        //            return response2;
        //        }

        //        if (resObj.responseResult.responseCode != "000")

        //        {
        //            var cres2 = new CommonResponse();
        //            cres2.status = false;
        //            cres2.ReponseCode = 3;
        //            cres2.Message = resObj.responseResult.responseCode;
        //            cres2.responseMessage = resObj.responseResult.responseCode;
        //            var response2 = Request.CreateResponse<CommonResponse>(HttpStatusCode.BadGateway, cres2);

        //            return response2;
        //        }


        //        lodgeBillReqDB.update(lodgeReq.cipsTransactionDetail.instructionId, resObj.responseResult.responseCode, JsonConvert.SerializeObject(lodgeReq), JsonConvert.SerializeObject(resObj), "", "");



        //        NCHLConsumerResp consumerResp = convertNPSLodgeToConsumerResp(resObj, VendorApiType);
        //        consumerResp.instructionId = lodgeReq.cipsTransactionDetail.instructionId;
        //        consumerResp.status = true;
        //        consumerResp.reponseCode = 1;

        //        response = Request.CreateResponse<NCHLConsumerResp>(HttpStatusCode.OK, consumerResp);
        //        consumerResp.serviceCharge = 0;

        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Info("SubmitNCHLRequest caught exception: " + ex.ToString());

        //        //   throw;
        //    }
        //    return response;

        //}
    }
}
