//using log4net;
//using MyPay.API.Models;
//using MyPay.API.Models.State;
//using MyPay.Models.Add;
//using MyPay.Models.Common;
//using MyPay.Models.Get;
//using MyPay.Models.Miscellaneous;
//using MyPay.Models.Response;
//using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
//using MyPay.Repository;
//using Newtonsoft.Json;
//using System;
//using System.Data.Entity.Validation;
//using System.IO;
//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Http;

//namespace MyPay.API.Controllers
//{
//    public class CreditCardPaymentController : ApiController
//    {
//        private static ILog log = LogManager.GetLogger(typeof(CreditCardPaymentController));
//        [System.Web.Http.HttpPost]
//        public HttpResponseMessage CreditCardPayment(Req_CreditCardPayment user)
//        {
//            log.Info($"{System.DateTime.Now.ToString()} inside CreditCardPayment" + Environment.NewLine);
//            CommonResponse cres = new CommonResponse();
//            Res_CreditCardIsuuerList result = new Res_CreditCardIsuuerList();
//            var response = Request.CreateResponse<Res_CreditCardIsuuerList>(System.Net.HttpStatusCode.BadRequest, result);

//            var userInput = getRawPostData().Result;

//            try
//            {
//                if (Request.Headers.Authorization == null)
//                {
//                    string results = "Un-Authorized Request";
//                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                    response.StatusCode = HttpStatusCode.BadRequest;
//                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                    return response;
//                }
//                else
//                {
//                    string md5hash = Common.getHashMD5(userInput); //Common.CheckHash(user);

//                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
//                    if (results != "Success")
//                    {
//                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
//                        response.StatusCode = HttpStatusCode.BadRequest;
//                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                        return response;
//                    }
//                    else
//                    {
//                        // string UserInput = getRawPostData().Result;
//                        string CommonResult = "";
//                        AddUserLoginWithPin resuser = new AddUserLoginWithPin();
//                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
//                        int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.Credit_Card_Payment;
//                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
//                        {
//                            Int64 memId = Convert.ToInt64(user.MemberId);
//                            int Type = Convert.ToInt32(user.PaymentMode);
//                            resuser = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, true, user.Amount.ToString(), true, user.Mpin);
//                            if (CommonResult.ToLower() != "success")
//                            {
//                                CommonResponse cres1 = new CommonResponse();
//                                cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
//                                response.StatusCode = HttpStatusCode.BadRequest;
//                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
//                                return response;
//                            }
//                        }
//                        else
//                        {
//                            CommonResponse cres1 = new CommonResponse();
//                            cres1 = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
//                            return response;
//                        }
//                        if (user.PaymentMode != "0" && user.PaymentMode != "1" && user.PaymentMode != "4") // WALLET AND MPCOINS
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage("Credit Card Payment with Link Bank is Unavailable. Please try again later!");
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            return response;
//                        }
//                        else if (string.IsNullOrEmpty(user.Code))
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage("Code Not Found");
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            return response;
//                        }
//                        else if (user.Amount == 0)
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage("Please enter amount");
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            return response;
//                        }
//                        else if (user.ServiceCharge == 0)
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage("Please enter service charge");
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            return response;
//                        }
//                        else if (string.IsNullOrEmpty(user.CardNumber))
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage("Please enter card number");
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            return response;
//                        }
//                        else if (string.IsNullOrEmpty(user.Name))
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage("Please enter name");
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            return response;
//                        }
//                        else if (user.MemberId == 0)
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage("Please enter memberid");
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            return response;
//                        }
//                        if (resuser.Id == 0)
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage("User Not Found");
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            return response;
//                        }
//                        if (user.Amount < 105 || user.Amount > 1000000)
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage("Amount should be between 105 to 1000000");
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            return response;
//                        }
//                        //decimal totalamount = (user.Amount + user.ServiceCharge); 


//                        user.Amount = user.Amount - user.ServiceCharge;
//                        decimal totalamount = (user.Amount);

//                        AddCalculateServiceChargeAndCashback objOut = Common.CalculateNetAmountWithServiceCharge(user.MemberId.ToString(), totalamount.ToString(), VendorAPIType.ToString());
//                        #region SaveAPIRequest
//                        string ApiResponse = string.Empty;

//                        int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.Credit_Card_Payment;
//                        AddVendor_API_Requests resVendor_API_Requests = new AddVendor_API_Requests();
//                        string TransactionId = new CommonHelpers().GenerateUniqueId();
//                        string Req_ReferenceNo = TransactionId;
//                        string JsonReq = string.Empty;
//                        string authenticationToken = Request.Headers.Authorization.Parameter;
//                        Common.authenticationToken = authenticationToken;
//                        resVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse("", Req_ReferenceNo, resuser.MemberId, resuser.FirstName + " " + resuser.LastName, JsonReq, authenticationToken, userInput, user.DeviceCode, user.PlatForm, VendorApiType);
//                        #endregion

//                        totalamount = totalamount + objOut.ServiceCharge + user.ServiceCharge;
//                        if (resuser.TotalAmount < totalamount)
//                        {
//                            cres = CommonApiMethod.ReturnBadRequestMessage("Insufficient Balance");
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            return response;
//                        }

//                        AddDepositOrders outobjectver = new AddDepositOrders();
//                        outobjectver.Amount = user.Amount;
//                        outobjectver.CreatedBy = Common.GetCreatedById(authenticationToken);
//                        outobjectver.CreatedByName = Common.GetCreatedByName(authenticationToken);
//                        outobjectver.TransactionId = TransactionId;
//                        outobjectver.RefferalsId = TransactionId;
//                        outobjectver.MemberId = Convert.ToInt64(user.MemberId);
//                        outobjectver.Type = (int)AddDepositOrders.DepositType.CreditCard_Payment;
//                        outobjectver.Remarks = "Credit Card Payment Initiate";
//                        outobjectver.Particulars = "Credit Card Payment Initiate";
//                        outobjectver.Status = (int)AddDepositOrders.DepositStatus.Incomplete;
//                        outobjectver.IsActive = true;
//                        outobjectver.IsApprovedByAdmin = true;
//                        outobjectver.ServiceCharges = (objOut.ServiceCharge);
//                        outobjectver.Platform = user.PlatForm;
//                        outobjectver.DeviceCode = user.DeviceCode;

//                        outobjectver.GatewayType = (int)VendorApi_CommonHelper.VendorTypes.Prabhu;
//                        outobjectver.GatewayName = VendorApi_CommonHelper.VendorTypes.Prabhu.ToString();

//                        Int64 Id = RepCRUD<AddDepositOrders, GetDepositOrders>.Insert(outobjectver, "depositorders");

//                        //var creditcardChargesController = new GetCreditCardChargesController();
//                        //Req_CreditCardCharge credicardChargesReq = new Req_CreditCardCharge();
//                        //credicardChargesReq.Amount = user.Amount;
//                        //credicardChargesReq.Code = user.Code;

//                        //var resp = creditcardChargesController.GetCreditCardIsuuer(credicardChargesReq);


//                        if (Id > 0)
//                        {
//                            string walletUniqueTransactionId = "";
//                            string transactionresult = WalletDeduct(resGetCouponsScratched, objOut, resuser, totalamount, authenticationToken, TransactionId, user.Amount, user.ServiceCharge, user.CardNumber, user.Code, resVendor_API_Requests, user.PlatForm, user.DeviceCode, VendorAPIType, ref walletUniqueTransactionId, Convert.ToInt32(user.PaymentMode));

//                            if (transactionresult == "success")
//                            {
//                                //user.Name = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
//                                GetCreditCardTransactionId res = RepPrabhu.MakeBillPayment(user.Amount.ToString(), "85", user.CardNumber, TransactionId, user.Code, user.Name, user.ServiceCharge.ToString(), "", "", "", "", "");
//                                //if (Common.ApplicationEnvironment.IsProduction == false)
//                                //{
//                                //    res.Code = "111";
//                                //    res.Message = "FAILED";
//                                //}
//                                AddDepositOrders outobject = new AddDepositOrders();
//                                GetDepositOrders inobject = new GetDepositOrders();
//                                inobject.Id = Id;
//                                AddDepositOrders resDeposit = RepCRUD<GetDepositOrders, AddDepositOrders>.GetRecord(nameof(Common.StoreProcedures.sp_DepositOrders_Get), inobject, outobject);
//                                if (resDeposit.Id > 0)
//                                {
//                                    if (res.Code == "000" && res.TransactionId != "")
//                                    {
//                                        resDeposit.RefferalsId = res.TransactionId;
//                                        resDeposit.Particulars = res.Message;
//                                        resDeposit.JsonResponse = JsonConvert.SerializeObject(res);
//                                        resDeposit.ResponseCode = res.Code;
//                                        resDeposit.Status = (int)AddDepositOrders.DepositStatus.Success;


//                                        WalletTransactions res_transaction = new WalletTransactions();
//                                        res_transaction.Reference = TransactionId;
//                                        if (res_transaction.GetRecord())
//                                        {
//                                            res_transaction.Status = (int)WalletTransactions.Statuses.Success;
//                                            res_transaction.GatewayStatus = WalletTransactions.Statuses.Success.ToString();
//                                            res_transaction.ResponseCode = res.Code;
//                                            res_transaction.VendorTransactionId = res.TransactionId;
//                                            if (res_transaction.Update())
//                                            {
//                                                RepCRUD<AddDepositOrders, GetDepositOrders>.Update(resDeposit, "depositorders");
//                                                res_transaction.Id = 0;
//                                                res_transaction.AddCashBack();
//                                                transactionresult = "Credit card Payment Successfull";
//                                                Common.AssignCoupons(res_transaction.MemberId, res_transaction.TransactionUniqueId);
//                                                #region SendEmailConfirmation
//                                                string mystring = File.ReadAllText(HttpContext.Current.Server.MapPath("/Templates/CreditCardPayment.html"));
//                                                string body = mystring;
//                                                body = body.Replace("##Amount##", res_transaction.Amount.ToString("0.00"));
//                                                body = body.Replace("##TransactionId##", res_transaction.TransactionUniqueId);
//                                                body = body.Replace("##ConsumerTransactionId##", res_transaction.Reference);
//                                                body = body.Replace("##Date##", Common.fnGetdatetimeFromInput(res_transaction.CreatedDate));
//                                                body = body.Replace("##Type##", WalletTransactions.Signs.Debit.ToString());
//                                                body = body.Replace("##Service##", ((VendorApi_CommonHelper.KhaltiAPIName)Convert.ToInt32(res_transaction.Type)).ToString().Replace("khalti", " ").Replace("_", " "));
//                                                body = body.Replace("##Status##", WalletTransactions.Statuses.Success.ToString());
//                                                body = body.Replace("##Cashback##", res_transaction.CashBack.ToString("0.00"));
//                                                body = body.Replace("##ServiceCharge##", res_transaction.ServiceCharge.ToString("0.00"));
//                                                body = body.Replace("##TotalAmount##", res_transaction.NetAmount.ToString("0.00"));
//                                                body = body.Replace("##Remarks##", res_transaction.Remarks.ToString().Replace("<", " "));
//                                                body = body.Replace("##From##", res_transaction.MemberName);
//                                                body = body.Replace("##To##", res_transaction.RecieverName);

//                                                string Subject = MyPay.Models.Common.Common.WebsiteName + " - Credit card Payment Successfull";
//                                                if (!string.IsNullOrEmpty(resuser.Email))
//                                                {
//                                                    body = body.Replace("##UserName##", resuser.FirstName);
//                                                    MyPay.Models.Common.Common.SendAsyncMail(resuser.Email, Subject, body);
//                                                }
//                                                #endregion
//                                            }
//                                            else
//                                            {
//                                                transactionresult = "Credit card Payment Transaction not updated ";
//                                            }
//                                        }
//                                        cres.responseMessage = res.Message;
//                                        cres.Message = "Success";
//                                        cres.Details = transactionresult;
//                                        cres.ReponseCode = 1;
//                                        cres.status = true;
//                                        cres.TransactionUniqueId = walletUniqueTransactionId;
//                                        response.StatusCode = HttpStatusCode.Created;
//                                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.Created, cres);

//                                        AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
//                                        GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
//                                        inobjApiResponse.Id = resVendor_API_Requests.Id;
//                                        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
//                                        if (resUpdateRecord != null && resUpdateRecord.Id != 0)
//                                        {
//                                            resUpdateRecord.Res_Khalti_Output = JsonConvert.SerializeObject(res);
//                                            resUpdateRecord.Res_Khalti_Message = resDeposit.Particulars;
//                                            resUpdateRecord.Res_Khalti_Id = res.TransactionId;
//                                            resUpdateRecord.Res_Khalti_State = AddDepositOrders.DepositStatus.Success.ToString();
//                                            resUpdateRecord.Res_Khalti_Status = true;
//                                            resUpdateRecord.Res_Output = JsonConvert.SerializeObject(cres);
//                                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
//                                        }
//                                        return response;
//                                    }
//                                    else if (!string.IsNullOrEmpty(res.Code) && res.Code != "777")
//                                    {
//                                        //string msgresult = WalletRefund(TransactionId, resuser, totalamount, authenticationToken, user.PlatForm, user.DeviceCode);
//                                        string msgresult = WalletRefund(TransactionId, resuser, totalamount, authenticationToken, user.PlatForm, user.DeviceCode);
//                                        if (res.TransactionId != "")
//                                        {
//                                            resDeposit.RefferalsId = res.TransactionId;
//                                        }
//                                        resDeposit.Particulars = res.Message;
//                                        resDeposit.JsonResponse = JsonConvert.SerializeObject(res);
//                                        resDeposit.ResponseCode = res.Code;
//                                        resDeposit.Status = (int)AddDepositOrders.DepositStatus.Failed;
//                                        //resDeposit.Status = (int)AddDepositOrders.DepositStatus.Failed;

//                                        RepCRUD<AddDepositOrders, GetDepositOrders>.Update(resDeposit, "depositorders");

//                                        Common.AddLogs(resDeposit.Particulars, false, Convert.ToInt32(AddLog.LogType.Credit_Card_Payment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, user.PlatForm, user.DeviceCode);

//                                        cres = CommonApiMethod.ReturnBadRequestMessage(resDeposit.Particulars);

//                                        AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
//                                        GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
//                                        inobjApiResponse.Id = resVendor_API_Requests.Id;
//                                        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
//                                        if (resUpdateRecord != null && resUpdateRecord.Id != 0)
//                                        {
//                                            resUpdateRecord.Res_Khalti_Message = msgresult;
//                                            // resUpdateRecord.Res_Khalti_Message = msgresult;
//                                            resUpdateRecord.Res_Khalti_Id = resDeposit.RefferalsId;
//                                            resUpdateRecord.Res_Khalti_State = AddDepositOrders.DepositStatus.Pending.ToString(); //AddDepositOrders.DepositStatus.Refund.ToString();
//                                                                                                                                  //resUpdateRecord.Res_Khalti_State = AddDepositOrders.DepositStatus.Refund.ToString();

//                                            resUpdateRecord.Res_Khalti_Status = false;
//                                            resUpdateRecord.Res_Output = JsonConvert.SerializeObject(cres);
//                                            resUpdateRecord.Res_Khalti_Output = JsonConvert.SerializeObject(res);
//                                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
//                                        }
//                                        response.StatusCode = HttpStatusCode.BadRequest;
//                                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                                    }
//                                    else
//                                    {
//                                        //string msgresult = WalletRefund(TransactionId, resuser, totalamount, authenticationToken, user.PlatForm, user.DeviceCode);
//                                        //string msgresult = WalletRefund(TransactionId, resuser, totalamount, authenticationToken, user.PlatForm, user.DeviceCode);
//                                        if (res.TransactionId != "")
//                                        {
//                                            resDeposit.RefferalsId = res.TransactionId;
//                                        }
//                                        resDeposit.Particulars = res.Message;
//                                        resDeposit.JsonResponse = JsonConvert.SerializeObject(res);
//                                        resDeposit.ResponseCode = res.Code;
//                                        resDeposit.Status = (int)AddDepositOrders.DepositStatus.Pending; // (int)AddDepositOrders.DepositStatus.Failed;
//                                        //resDeposit.Status = (int)AddDepositOrders.DepositStatus.Failed;

//                                        RepCRUD<AddDepositOrders, GetDepositOrders>.Update(resDeposit, "depositorders");

//                                        Common.AddLogs(resDeposit.Particulars, false, Convert.ToInt32(AddLog.LogType.Credit_Card_Payment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, user.PlatForm, user.DeviceCode);

//                                        cres = CommonApiMethod.ReturnBadRequestMessage(resDeposit.Particulars);

//                                        AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
//                                        GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
//                                        inobjApiResponse.Id = resVendor_API_Requests.Id;
//                                        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
//                                        if (resUpdateRecord != null && resUpdateRecord.Id != 0)
//                                        {
//                                            //resUpdateRecord.Res_Khalti_Message = msgresult;
//                                            // resUpdateRecord.Res_Khalti_Message = msgresult;
//                                            resUpdateRecord.Res_Khalti_Id = resDeposit.RefferalsId;
//                                            resUpdateRecord.Res_Khalti_State = AddDepositOrders.DepositStatus.Pending.ToString(); //AddDepositOrders.DepositStatus.Refund.ToString();
//                                            //resUpdateRecord.Res_Khalti_State = AddDepositOrders.DepositStatus.Refund.ToString();

//                                            resUpdateRecord.Res_Khalti_Status = false;
//                                            resUpdateRecord.Res_Output = JsonConvert.SerializeObject(cres);
//                                            resUpdateRecord.Res_Khalti_Output = JsonConvert.SerializeObject(res);
//                                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
//                                        }
//                                        response.StatusCode = HttpStatusCode.BadRequest;
//                                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                                    }
//                                    return response;
//                                }
//                                else
//                                {
//                                    Common.AddLogs("Deposit Order Not Found", false, Convert.ToInt32(AddLog.LogType.Credit_Card_Payment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, user.PlatForm, user.DeviceCode);
//                                    cres = CommonApiMethod.ReturnBadRequestMessage("Deposit Order Not Found");
//                                    response.StatusCode = HttpStatusCode.BadRequest;
//                                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                                    return response;
//                                }
//                            }
//                            else
//                            {
//                                cres = CommonApiMethod.ReturnBadRequestMessage(transactionresult);

//                                AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
//                                GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
//                                inobjApiResponse.Id = resVendor_API_Requests.Id;
//                                AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
//                                if (resUpdateRecord != null && resUpdateRecord.Id != 0 && resVendor_API_Requests.Id != 0)
//                                {
//                                    resUpdateRecord.Res_Khalti_State = AddDepositOrders.DepositStatus.Failed.ToString();
//                                    resUpdateRecord.Res_Khalti_Status = false;
//                                    resUpdateRecord.Res_Output = JsonConvert.SerializeObject(cres);
//                                    RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
//                                }
//                                response.StatusCode = HttpStatusCode.BadRequest;
//                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                                Common.AddLogs(cres.Message, false, Convert.ToInt32(AddLog.LogType.Credit_Card_Payment), user.MemberId, resuser.FirstName + " " + resuser.LastName, true, user.PlatForm, user.DeviceCode);

//                                return response;
//                            }
//                        }
//                        else
//                        {
//                            Common.AddLogs("Deposit Order Not Created", false, Convert.ToInt32(AddLog.LogType.Credit_Card_Payment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, user.PlatForm, user.DeviceCode);
//                            cres = CommonApiMethod.ReturnBadRequestMessage("Deposit Order Not Created");
//                            response.StatusCode = HttpStatusCode.BadRequest;
//                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//                            return response;
//                        }
//                    }

//                }
//            }
//            catch (DbEntityValidationException e)
//            {
//                foreach (var eve in e.EntityValidationErrors)
//                {
//                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                    foreach (var ve in eve.ValidationErrors)
//                    {
//                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                            ve.PropertyName, ve.ErrorMessage);
//                    }
//                }
//                log.Error($"{System.DateTime.Now.ToString()} CreditCardPayment {e.ToString()} " + Environment.NewLine);
//                throw;
//            }
//            catch (Exception ex)
//            {
//                log.Error($"{System.DateTime.Now.ToString()} CreditCardPayment {ex.ToString()} " + Environment.NewLine);
//                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
//            }

//        }

//        private async Task<String> getRawPostData()
//        {
//            using (var contentStream = await this.Request.Content.ReadAsStreamAsync())
//            {
//                contentStream.Seek(0, SeekOrigin.Begin);
//                using (var sr = new StreamReader(contentStream))
//                {
//                    return sr.ReadToEnd();
//                }
//            }
//        }

//        public static string WalletDeduct(AddCouponsScratched resCoupon, AddCalculateServiceChargeAndCashback objOut, AddUserLoginWithPin resuser, decimal totalamount, string authenticationtoken, string TransactionId, decimal amount, decimal servicecharge, string cardnumber, string code, AddVendor_API_Requests resVendor_API_Requests, string PlatForm, string DeviceCode, Int32 VendorApiType, ref string uniqueTransactionId, Int32 WalletType = 1)
//        {
//            string result = "";
//            try
//            {
//                decimal CouponDeduct = 0;
//                decimal WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resuser.TotalAmount) - Convert.ToDecimal(totalamount));
//                decimal RewardPointBalance = Convert.ToDecimal(resuser.TotalRewardPoints);
//                if (WalletType == 0 || WalletType == ((int)WalletTransactions.WalletTypes.Wallet))
//                {
//                    WalletType = ((int)WalletTransactions.WalletTypes.Wallet);
//                    CouponDeduct = CouponDeduct + Convert.ToDecimal((Convert.ToDecimal(amount) * resCoupon.CouponPercentage) / 100);
//                    WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resuser.TotalAmount) - Convert.ToDecimal(totalamount) - CouponDeduct);
//                    RewardPointBalance = Convert.ToDecimal(resuser.TotalRewardPoints);
//                }
//                else if (WalletType == ((int)WalletTransactions.WalletTypes.MPCoins))
//                {
//                    CouponDeduct = CouponDeduct + Convert.ToDecimal((Convert.ToDecimal(amount) * resCoupon.CouponPercentage) / 100);
//                    WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resuser.TotalAmount) - ((Convert.ToDecimal(totalamount) - objOut.MPCoinsDebit - CouponDeduct)));
//                    RewardPointBalance = Convert.ToDecimal(Convert.ToDecimal(resuser.TotalRewardPoints) - objOut.MPCoinsDebit);
//                }
//                WalletTransactions res_transaction = new WalletTransactions();
//                res_transaction.VendorTransactionId = TransactionId;
//                if (!res_transaction.GetRecordCheckExists())
//                {
//                    res_transaction.MemberId = Convert.ToInt64(resuser.MemberId);
//                    res_transaction.ContactNumber = resuser.ContactNumber;
//                    res_transaction.MemberName = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
//                    if (WalletType == ((int)WalletTransactions.WalletTypes.MPCoins))
//                    {
//                        res_transaction.Amount = Convert.ToDecimal((amount + servicecharge)) - objOut.MPCoinsDebit;
//                    }
//                    else
//                    {
//                        res_transaction.Amount = Convert.ToDecimal((amount + servicecharge)) - Convert.ToDecimal(CouponDeduct);
//                    }
//                    res_transaction.UpdateBy = Convert.ToInt64(resuser.MemberId);
//                    res_transaction.UpdateByName = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
//                    res_transaction.CurrentBalance = WalletBalance;
//                    res_transaction.RewardPointBalance = RewardPointBalance;
//                    res_transaction.CreatedBy = Common.GetCreatedById(authenticationtoken);
//                    res_transaction.CreatedByName = Common.GetCreatedByName(authenticationtoken);
//                    res_transaction.TransactionUniqueId = new CommonHelpers().GenerateUniqueId();
//                    uniqueTransactionId = res_transaction.TransactionUniqueId;
//                    res_transaction.VendorTransactionId = new CommonHelpers().GenerateUniqueId();
//                    res_transaction.Reference = TransactionId;
//                    //res_transaction.BatchTransactionId = res.cipsBatchResponse.batchId;
//                    //res_transaction.TxnInstructionId = res.cipsTxnResponseList[0].instructionId;
//                    res_transaction.Remarks = "Credit Card Payment Successfully Deposit";
//                    res_transaction.GatewayStatus = WalletTransactions.Statuses.Pending.ToString();
//                    res_transaction.ResponseCode = "";
//                    res_transaction.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Credit_Card_Payment;
//                    res_transaction.Description = "Credit Card Payment Successfully Deposit";
//                    res_transaction.Status = (int)WalletTransactions.Statuses.Pending;
//                    res_transaction.IsApprovedByAdmin = true;
//                    res_transaction.IsActive = true;
//                    res_transaction.Sign = Convert.ToInt16(WalletTransactions.Signs.Debit);
//                    res_transaction.RecieverName = Common.ConnectIPs_AccountNumber;
//                    res_transaction.TransferType = (int)WalletTransactions.TransferTypes.Sender;
//                    res_transaction.RecieverAccountNo = Common.ConnectIPs_AccountNumber;
//                    res_transaction.RecieverBankCode = Common.ConnectIps_BankId;
//                    res_transaction.RecieverBranch = Common.ConnectIPs_BranchName;
//                    res_transaction.CardNumber = cardnumber;
//                    res_transaction.CardType = code;
//                    res_transaction.ServiceCharge = (objOut.ServiceCharge + servicecharge);
//                    res_transaction.NetAmount = totalamount;
//                    res_transaction.Purpose = "Credit Card Payment";
//                    res_transaction.Platform = PlatForm;
//                    res_transaction.WalletType = WalletType;
//                    res_transaction.VendorType = (int)VendorApi_CommonHelper.VendorTypes.Prabhu;

//                    res_transaction.RecieverBankName = Common.ConnectIPs_BankName;
//                    res_transaction.RecieverBranchName = Common.ConnectIPs_BranchName;

//                    if ((WalletType == ((int)WalletTransactions.WalletTypes.Wallet) || WalletType == ((int)WalletTransactions.WalletTypes.FonePay)))
//                    {
//                        res_transaction.TransactionAmount = objOut.Amount;
//                        res_transaction.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
//                        res_transaction.CouponCode = resCoupon.CouponCode;
//                        res_transaction.CouponDiscount = CouponDeduct;
//                    }
//                    else if (WalletType == ((int)WalletTransactions.WalletTypes.MPCoins))
//                    {
//                        res_transaction.TransactionAmount = objOut.Amount;
//                        res_transaction.MPCoinsDebit = objOut.MPCoinsDebit;
//                        res_transaction.RewardPointBalance = (resuser.TotalRewardPoints) - objOut.MPCoinsDebit;
//                        res_transaction.WalletType = (int)WalletTransactions.WalletTypes.MPCoins;
//                        res_transaction.CouponDiscount = CouponDeduct;
//                    }
//                    if (res_transaction.Add())
//                    {
//                        Common.AddLogs("Credit Card Payment Successfully Deposit", false, Convert.ToInt32(AddLog.LogType.Credit_Card_Payment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, PlatForm, DeviceCode);
//                        result = "success";
//                    }
//                    else
//                    {
//                        Common.AddLogs("Something Went Wrong Payment Not Sent", false, Convert.ToInt32(AddLog.LogType.Credit_Card_Payment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, PlatForm, DeviceCode);
//                        result = "Something Went Wrong Payment Not Sent";
//                    }
//                }
//                else
//                {

//                    Common.AddLogs("Transaction Sent Already", false, Convert.ToInt32(AddLog.LogType.Credit_Card_Payment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, PlatForm, DeviceCode);
//                    result = "Transaction Sent Already";
//                }
//            }
//            catch (Exception ex)
//            {
//                result = ex.Message;
//            }
//            return result;
//        }

//        public static string WalletRefund(string TransactionId, AddUserLoginWithPin resuser, decimal totalamount, string authenticationtoken, string Platform, string DeviceCode)
//        {
//            string result = "";
//            try
//            {
//                WalletTransactions res_transaction = new WalletTransactions();
//                res_transaction.Reference = TransactionId;
//                if (res_transaction.GetRecord())
//                {
//                    WalletTransactions newtra = new WalletTransactions();
//                    decimal WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resuser.TotalAmount));
//                    newtra.MemberId = Convert.ToInt64(resuser.MemberId);
//                    newtra.ContactNumber = resuser.ContactNumber;
//                    newtra.MemberName = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
//                    newtra.Amount = totalamount; //(res_transaction.NetAmount);
//                    newtra.UpdateBy = Convert.ToInt64(resuser.MemberId);
//                    newtra.UpdateByName = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
//                    newtra.CurrentBalance = WalletBalance;
//                    newtra.CreatedBy = Common.GetCreatedById(authenticationtoken);
//                    newtra.CreatedByName = Common.GetCreatedByName(authenticationtoken);
//                    newtra.TransactionUniqueId = new CommonHelpers().GenerateUniqueId();
//                    newtra.VendorTransactionId = new CommonHelpers().GenerateUniqueId();
//                    newtra.Reference = TransactionId;
//                    newtra.ParentTransactionId = res_transaction.TransactionUniqueId;
//                    newtra.Remarks = "Credit Card Payment Successfully Refunded for Transactino ID: " + res_transaction.TransactionUniqueId;
//                    newtra.GatewayStatus = WalletTransactions.Statuses.Success.ToString();
//                    newtra.ResponseCode = "";
//                    newtra.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Credit_Card_Payment;
//                    newtra.Description = "Credit Card Payment Successfully Refunded";
//                    newtra.Status = (int)WalletTransactions.Statuses.Refund;
//                    newtra.IsApprovedByAdmin = true;
//                    newtra.IsActive = true;
//                    newtra.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
//                    newtra.RecieverName = Common.ConnectIPs_AccountNumber;
//                    newtra.TransferType = (int)WalletTransactions.TransferTypes.Sender;
//                    newtra.RecieverAccountNo = Common.ConnectIPs_AccountNumber;
//                    newtra.RecieverBankCode = Common.ConnectIps_BankId;
//                    newtra.RecieverBranch = Common.ConnectIPs_BranchName;
//                    newtra.CardNumber = res_transaction.CardNumber;
//                    newtra.CardType = res_transaction.CardType;
//                    newtra.ServiceCharge = (res_transaction.ServiceCharge);
//                    newtra.NetAmount = totalamount;
//                    newtra.Purpose = "Credit Card Payment Refund";
//                    newtra.Platform = Platform;
//                    newtra.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
//                    newtra.VendorType = (int)VendorApi_CommonHelper.VendorTypes.MyPay;


//                    newtra.Amount = totalamount; //Convert.ToDecimal(res_transaction.NetAmount);
//                    newtra.RewardPoint = res_transaction.RewardPoint;
//                    newtra.RewardPointBalance = res_transaction.RewardPointBalance;
//                    if (res_transaction.WalletType == ((int)WalletTransactions.WalletTypes.MPCoins))
//                    {
//                        newtra.Amount = Convert.ToDecimal(res_transaction.Amount) + Convert.ToDecimal(res_transaction.ServiceCharge);
//                        newtra.RewardPoint = res_transaction.RewardPoint;
//                        newtra.MPCoinsDebit = res_transaction.MPCoinsDebit;
//                        newtra.RewardPointBalance = res_transaction.RewardPointBalance;
//                        WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resuser.TotalAmount));
//                    }

//                    if (newtra.Add())
//                    {
//                        res_transaction.Remarks = "Credit Card Payment Failed";
//                        res_transaction.Status = (int)WalletTransactions.Statuses.Failed;
//                        res_transaction.GatewayStatus = WalletTransactions.Statuses.Failed.ToString();
//                        if (res_transaction.Update())
//                        {
//                            result = "success";
//                        }
//                        else
//                        {
//                            result = "Refunded But Transaction Not Updated";
//                        }
//                    }
//                    else
//                    {
//                        Common.AddLogs("Something Went Wrong Payment Not Sent", false, Convert.ToInt32(AddLog.LogType.Credit_Card_Payment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, Platform, DeviceCode);
//                        result = "Something Went Wrong Payment Not Sent";
//                    }
//                }
//                else
//                {
//                    result = "Transaction Not Found For Refund";
//                }
//            }
//            catch (Exception ex)
//            {
//                result = ex.Message;
//            }
//            return result;
//        }
//    }
//}


//////using log4net;
//////using MyPay.API.Models;
//////using MyPay.API.Models.State;
//////using MyPay.Models.Add;
//////using MyPay.Models.Common;
//////using MyPay.Models.Get;
//////using MyPay.Models.Miscellaneous;
//////using MyPay.Models.Response;
//////using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
//////using MyPay.Repository;
//////using Newtonsoft.Json;
//////using System;
//////using System.Data.Entity.Validation;
//////using System.IO;
//////using System.Net;
//////using System.Net.Http;
//////using System.Threading.Tasks;
//////using System.Web;
//////using System.Web.Http;

//////namespace MyPay.API.Controllers
//////{
//////    public class CreditCardPaymentController : ApiController
//////    {
//////        private static ILog log = LogManager.GetLogger(typeof(CreditCardPaymentController));
//////        [System.Web.Http.HttpPost]
//////        public HttpResponseMessage CreditCardPayment(Req_CreditCardPayment user)
//////        {
//////            log.Info($"{System.DateTime.Now.ToString()} inside CreditCardPayment" + Environment.NewLine);
//////            CommonResponse cres = new CommonResponse();
//////            Res_CreditCardIsuuerList result = new Res_CreditCardIsuuerList();
//////            var response = Request.CreateResponse<Res_CreditCardIsuuerList>(System.Net.HttpStatusCode.BadRequest, result);

//////            var userInput = getRawPostData().Result;

//////            try
//////            {
//////                if (Request.Headers.Authorization == null)
//////                {
//////                    string results = "Un-Authorized Request";
//////                    cres = CommonApiMethod.ReturnBadRequestMessage(results);
//////                    response.StatusCode = HttpStatusCode.BadRequest;
//////                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//////                    return response;
//////                }
//////                else
//////                {
//////                    string md5hash = Common.getHashMD5(userInput); //Common.CheckHash(user);

//////                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
//////                    if (results != "Success")
//////                    {
//////                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
//////                        response.StatusCode = HttpStatusCode.BadRequest;
//////                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//////                        return response;
//////                    }
//////                    else
//////                    {
//////                       // string UserInput = getRawPostData().Result;
//////                        string CommonResult = "";
//////                        AddUserLoginWithPin resuser = new AddUserLoginWithPin();
//////                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
//////                        int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.Credit_Card_Payment;
//////                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
//////                        {
//////                            Int64 memId = Convert.ToInt64(user.MemberId);
//////                            int Type = Convert.ToInt32(user.PaymentMode);
//////                            resuser = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, userInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, true, true, user.Amount.ToString(), true, user.Mpin);
//////                            if (CommonResult.ToLower() != "success")
//////                            {
//////                                CommonResponse cres1 = new CommonResponse();
//////                                cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
//////                                response.StatusCode = HttpStatusCode.BadRequest;
//////                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
//////                                return response;
//////                            }
//////                        }
//////                        else
//////                        {
//////                            CommonResponse cres1 = new CommonResponse();
//////                            cres1 = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
//////                            response.StatusCode = HttpStatusCode.BadRequest;
//////                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
//////                            return response;
//////                        }
//////                        if (user.PaymentMode != "0" && user.PaymentMode != "1" && user.PaymentMode != "4") // WALLET AND MPCOINS
//////                        {
//////                            cres = CommonApiMethod.ReturnBadRequestMessage("Credit Card Payment with Link Bank is Unavailable. Please try again later!");
//////                            response.StatusCode = HttpStatusCode.BadRequest;
//////                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//////                            return response;
//////                        }
//////                        else if (string.IsNullOrEmpty(user.Code))
//////                        {
//////                            cres = CommonApiMethod.ReturnBadRequestMessage("Code Not Found");
//////                            response.StatusCode = HttpStatusCode.BadRequest;
//////                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//////                            return response;
//////                        }
//////                        else if (user.Amount == 0)
//////                        {
//////                            cres = CommonApiMethod.ReturnBadRequestMessage("Please enter amount");
//////                            response.StatusCode = HttpStatusCode.BadRequest;
//////                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//////                            return response;
//////                        }
//////                        else if (user.ServiceCharge == 0)
//////                        {
//////                            cres = CommonApiMethod.ReturnBadRequestMessage("Please enter service charge");
//////                            response.StatusCode = HttpStatusCode.BadRequest;
//////                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//////                            return response;
//////                        }
//////                        else if (string.IsNullOrEmpty(user.CardNumber))
//////                        {
//////                            cres = CommonApiMethod.ReturnBadRequestMessage("Please enter card number");
//////                            response.StatusCode = HttpStatusCode.BadRequest;
//////                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//////                            return response;
//////                        }
//////                        else if (string.IsNullOrEmpty(user.Name))
//////                        {
//////                            cres = CommonApiMethod.ReturnBadRequestMessage("Please enter name");
//////                            response.StatusCode = HttpStatusCode.BadRequest;
//////                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//////                            return response;
//////                        }
//////                        else if (user.MemberId == 0)
//////                        {
//////                            cres = CommonApiMethod.ReturnBadRequestMessage("Please enter memberid");
//////                            response.StatusCode = HttpStatusCode.BadRequest;
//////                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//////                            return response;
//////                        }
//////                        if (resuser.Id == 0)
//////                        {
//////                            cres = CommonApiMethod.ReturnBadRequestMessage("User Not Found");
//////                            response.StatusCode = HttpStatusCode.BadRequest;
//////                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//////                            return response;
//////                        }
//////                        if (user.Amount < 105 || user.Amount > 1000000)
//////                        {
//////                            cres = CommonApiMethod.ReturnBadRequestMessage("Amount should be between 105 to 1000000");
//////                            response.StatusCode = HttpStatusCode.BadRequest;
//////                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//////                            return response;
//////                        }
//////                        //decimal totalamount = (user.Amount + user.ServiceCharge); 


//////                        user.Amount = user.Amount - user.ServiceCharge;
//////                        decimal totalamount = (user.Amount);

//////                        AddCalculateServiceChargeAndCashback objOut = Common.CalculateNetAmountWithServiceCharge(user.MemberId.ToString(), totalamount.ToString(), VendorAPIType.ToString());
//////                        #region SaveAPIRequest
//////                        string ApiResponse = string.Empty;

//////                        int VendorApiType = (int)VendorApi_CommonHelper.KhaltiAPIName.Credit_Card_Payment;
//////                        AddVendor_API_Requests resVendor_API_Requests = new AddVendor_API_Requests();
//////                        string TransactionId = new CommonHelpers().GenerateUniqueId();
//////                        string Req_ReferenceNo = TransactionId;
//////                        string JsonReq = string.Empty;
//////                        string authenticationToken = Request.Headers.Authorization.Parameter;
//////                        Common.authenticationToken = authenticationToken;
//////                        resVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse("", Req_ReferenceNo, resuser.MemberId, resuser.FirstName + " " + resuser.LastName, JsonReq, authenticationToken, userInput, user.DeviceCode, user.PlatForm, VendorApiType);
//////                        #endregion

//////                        totalamount = totalamount + objOut.ServiceCharge + user.ServiceCharge;
//////                        if (resuser.TotalAmount < totalamount)
//////                        {
//////                            cres = CommonApiMethod.ReturnBadRequestMessage("Insufficient Balance");
//////                            response.StatusCode = HttpStatusCode.BadRequest;
//////                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//////                            return response;
//////                        }

//////                        AddDepositOrders outobjectver = new AddDepositOrders();
//////                        outobjectver.Amount = user.Amount;
//////                        outobjectver.CreatedBy = Common.GetCreatedById(authenticationToken);
//////                        outobjectver.CreatedByName = Common.GetCreatedByName(authenticationToken);
//////                        outobjectver.TransactionId = TransactionId;
//////                        outobjectver.RefferalsId = TransactionId;
//////                        outobjectver.MemberId = Convert.ToInt64(user.MemberId);
//////                        outobjectver.Type = (int)AddDepositOrders.DepositType.CreditCard_Payment;
//////                        outobjectver.Remarks = "Credit Card Payment Initiate";
//////                        outobjectver.Particulars = "Credit Card Payment Initiate";
//////                        outobjectver.Status = (int)AddDepositOrders.DepositStatus.Incomplete;
//////                        outobjectver.IsActive = true;
//////                        outobjectver.IsApprovedByAdmin = true;
//////                        outobjectver.ServiceCharges = (objOut.ServiceCharge);
//////                        outobjectver.Platform = user.PlatForm;
//////                        outobjectver.DeviceCode = user.DeviceCode;

//////                        outobjectver.GatewayType = (int)VendorApi_CommonHelper.VendorTypes.Prabhu;
//////                        outobjectver.GatewayName = VendorApi_CommonHelper.VendorTypes.Prabhu.ToString();

//////                        Int64 Id = RepCRUD<AddDepositOrders, GetDepositOrders>.Insert(outobjectver, "depositorders");

//////                        //var creditcardChargesController = new GetCreditCardChargesController();
//////                        //Req_CreditCardCharge credicardChargesReq = new Req_CreditCardCharge();
//////                        //credicardChargesReq.Amount = user.Amount;
//////                        //credicardChargesReq.Code = user.Code;

//////                        //var resp = creditcardChargesController.GetCreditCardIsuuer(credicardChargesReq);


//////                        if (Id > 0)
//////                        {
//////                            string walletUniqueTransactionId = "";
//////                            string transactionresult = WalletDeduct(resGetCouponsScratched, objOut, resuser, totalamount, authenticationToken, TransactionId, user.Amount, user.ServiceCharge , user.CardNumber, user.Code, resVendor_API_Requests, user.PlatForm, user.DeviceCode, VendorAPIType, ref walletUniqueTransactionId, Convert.ToInt32(user.PaymentMode));

//////                            if (transactionresult == "success")
//////                            {
//////                                //user.Name = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
//////                                GetCreditCardTransactionId res = RepPrabhu.MakeBillPayment(user.Amount.ToString(), "85", user.CardNumber, TransactionId, user.Code, user.Name, user.ServiceCharge.ToString(), "", "", "", "", "");
//////                                //if (Common.ApplicationEnvironment.IsProduction == false)
//////                                //{
//////                                //    res.Code = "111";
//////                                //    res.Message = "FAILED";
//////                                //}
//////                                AddDepositOrders outobject = new AddDepositOrders();
//////                                GetDepositOrders inobject = new GetDepositOrders();
//////                                inobject.Id = Id;
//////                                AddDepositOrders resDeposit = RepCRUD<GetDepositOrders, AddDepositOrders>.GetRecord(nameof(Common.StoreProcedures.sp_DepositOrders_Get), inobject, outobject);
//////                                if (resDeposit.Id > 0)
//////                                {
//////                                    if (res.Code == "000" && res.TransactionId != "")
//////                                    {
//////                                        resDeposit.RefferalsId = res.TransactionId;
//////                                        resDeposit.Particulars = res.Message;
//////                                        resDeposit.JsonResponse = JsonConvert.SerializeObject(res);
//////                                        resDeposit.ResponseCode = res.Code;
//////                                        resDeposit.Status = (int)AddDepositOrders.DepositStatus.Success;


//////                                        WalletTransactions res_transaction = new WalletTransactions();
//////                                        res_transaction.Reference = TransactionId;
//////                                        if (res_transaction.GetRecord())
//////                                        {
//////                                            res_transaction.Status = (int)WalletTransactions.Statuses.Success;
//////                                            res_transaction.GatewayStatus = WalletTransactions.Statuses.Success.ToString();
//////                                            res_transaction.ResponseCode = res.Code;
//////                                            res_transaction.VendorTransactionId = res.TransactionId;
//////                                            if (res_transaction.Update())
//////                                            {
//////                                                RepCRUD<AddDepositOrders, GetDepositOrders>.Update(resDeposit, "depositorders");
//////                                                res_transaction.Id = 0;
//////                                                res_transaction.AddCashBack();
//////                                                transactionresult = "Credit card Payment Successfull";
//////                                                Common.AssignCoupons(res_transaction.MemberId, res_transaction.TransactionUniqueId);
//////                                                #region SendEmailConfirmation
//////                                                string mystring = File.ReadAllText(HttpContext.Current.Server.MapPath("/Templates/CreditCardPayment.html"));
//////                                                string body = mystring;
//////                                                body = body.Replace("##Amount##", res_transaction.Amount.ToString("0.00"));
//////                                                body = body.Replace("##TransactionId##", res_transaction.TransactionUniqueId);
//////                                                body = body.Replace("##ConsumerTransactionId##", res_transaction.Reference);
//////                                                body = body.Replace("##Date##", Common.fnGetdatetimeFromInput(res_transaction.CreatedDate));
//////                                                body = body.Replace("##Type##", WalletTransactions.Signs.Debit.ToString());
//////                                                body = body.Replace("##Service##", ((VendorApi_CommonHelper.KhaltiAPIName)Convert.ToInt32(res_transaction.Type)).ToString().Replace("khalti", " ").Replace("_", " "));
//////                                                body = body.Replace("##Status##", WalletTransactions.Statuses.Success.ToString());
//////                                                body = body.Replace("##Cashback##", res_transaction.CashBack.ToString("0.00"));
//////                                                body = body.Replace("##ServiceCharge##", res_transaction.ServiceCharge.ToString("0.00"));
//////                                                body = body.Replace("##TotalAmount##", res_transaction.NetAmount.ToString("0.00"));
//////                                                body = body.Replace("##Remarks##", res_transaction.Remarks.ToString().Replace("<", " "));
//////                                                body = body.Replace("##From##", res_transaction.MemberName);
//////                                                body = body.Replace("##To##", res_transaction.RecieverName);

//////                                                string Subject = MyPay.Models.Common.Common.WebsiteName + " - Credit card Payment Successfull";
//////                                                if (!string.IsNullOrEmpty(resuser.Email))
//////                                                {
//////                                                    body = body.Replace("##UserName##", resuser.FirstName);
//////                                                    MyPay.Models.Common.Common.SendAsyncMail(resuser.Email, Subject, body);
//////                                                }
//////                                                #endregion
//////                                            }
//////                                            else
//////                                            {
//////                                                transactionresult = "Credit card Payment Transaction not updated ";
//////                                            }
//////                                        }
//////                                        cres.responseMessage = res.Message;
//////                                        cres.Message = "Success";
//////                                        cres.Details = transactionresult;
//////                                        cres.ReponseCode = 1;
//////                                        cres.status = true;
//////                                        cres.TransactionUniqueId = walletUniqueTransactionId;
//////                                        response.StatusCode = HttpStatusCode.Created;
//////                                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.Created, cres);

//////                                        AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
//////                                        GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
//////                                        inobjApiResponse.Id = resVendor_API_Requests.Id;
//////                                        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
//////                                        if (resUpdateRecord != null && resUpdateRecord.Id != 0)
//////                                        {
//////                                            resUpdateRecord.Res_Khalti_Output = JsonConvert.SerializeObject(res);
//////                                            resUpdateRecord.Res_Khalti_Message = resDeposit.Particulars;
//////                                            resUpdateRecord.Res_Khalti_Id = res.TransactionId;
//////                                            resUpdateRecord.Res_Khalti_State = AddDepositOrders.DepositStatus.Success.ToString();
//////                                            resUpdateRecord.Res_Khalti_Status = true;
//////                                            resUpdateRecord.Res_Output = JsonConvert.SerializeObject(cres);
//////                                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
//////                                        }
//////                                        return response;
//////                                    } else if (!string.IsNullOrEmpty (res.Code) && res.Code != "777")
//////                                            {
//////                                                //string msgresult = WalletRefund(TransactionId, resuser, totalamount, authenticationToken, user.PlatForm, user.DeviceCode);
//////                                                string msgresult = WalletRefund(TransactionId, resuser, totalamount, authenticationToken, user.PlatForm, user.DeviceCode);
//////                                                if (res.TransactionId != "")
//////                                                {
//////                                                    resDeposit.RefferalsId = res.TransactionId;
//////                                                }
//////                                                resDeposit.Particulars = res.Message;
//////                                                resDeposit.JsonResponse = JsonConvert.SerializeObject(res);
//////                                                resDeposit.ResponseCode = res.Code;
//////                                                resDeposit.Status = (int)AddDepositOrders.DepositStatus.Failed;
//////                                                //resDeposit.Status = (int)AddDepositOrders.DepositStatus.Failed;

//////                                                RepCRUD<AddDepositOrders, GetDepositOrders>.Update(resDeposit, "depositorders");

//////                                                Common.AddLogs(resDeposit.Particulars, false, Convert.ToInt32(AddLog.LogType.Credit_Card_Payment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, user.PlatForm, user.DeviceCode);

//////                                                cres = CommonApiMethod.ReturnBadRequestMessage(resDeposit.Particulars);

//////                                                AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
//////                                                GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
//////                                                inobjApiResponse.Id = resVendor_API_Requests.Id;
//////                                                AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
//////                                                if (resUpdateRecord != null && resUpdateRecord.Id != 0)
//////                                                {
//////                                                    resUpdateRecord.Res_Khalti_Message = msgresult;
//////                                                    // resUpdateRecord.Res_Khalti_Message = msgresult;
//////                                                    resUpdateRecord.Res_Khalti_Id = resDeposit.RefferalsId;
//////                                                    resUpdateRecord.Res_Khalti_State = AddDepositOrders.DepositStatus.Pending.ToString(); //AddDepositOrders.DepositStatus.Refund.ToString();
//////                                                                                                                                          //resUpdateRecord.Res_Khalti_State = AddDepositOrders.DepositStatus.Refund.ToString();

//////                                                    resUpdateRecord.Res_Khalti_Status = false;
//////                                                    resUpdateRecord.Res_Output = JsonConvert.SerializeObject(cres);
//////                                                    resUpdateRecord.Res_Khalti_Output = JsonConvert.SerializeObject(res);
//////                                                    RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
//////                                                }
//////                                                response.StatusCode = HttpStatusCode.BadRequest;
//////                                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//////                                            }
//////                                    else
//////                                    {
//////                                        //string msgresult = WalletRefund(TransactionId, resuser, totalamount, authenticationToken, user.PlatForm, user.DeviceCode);
//////                                        //string msgresult = WalletRefund(TransactionId, resuser, totalamount, authenticationToken, user.PlatForm, user.DeviceCode);
//////                                        if (res.TransactionId != "")
//////                                        {
//////                                            resDeposit.RefferalsId = res.TransactionId;
//////                                        }
//////                                        resDeposit.Particulars = res.Message;
//////                                        resDeposit.JsonResponse = JsonConvert.SerializeObject(res);
//////                                        resDeposit.ResponseCode = res.Code;
//////                                        resDeposit.Status = (int)AddDepositOrders.DepositStatus.Pending; // (int)AddDepositOrders.DepositStatus.Failed;
//////                                        //resDeposit.Status = (int)AddDepositOrders.DepositStatus.Failed;

//////                                        RepCRUD<AddDepositOrders, GetDepositOrders>.Update(resDeposit, "depositorders");

//////                                        Common.AddLogs(resDeposit.Particulars, false, Convert.ToInt32(AddLog.LogType.Credit_Card_Payment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, user.PlatForm, user.DeviceCode);

//////                                        cres = CommonApiMethod.ReturnBadRequestMessage(resDeposit.Particulars);

//////                                        AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
//////                                        GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
//////                                        inobjApiResponse.Id = resVendor_API_Requests.Id;
//////                                        AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
//////                                        if (resUpdateRecord != null && resUpdateRecord.Id != 0)
//////                                        {
//////                                            //resUpdateRecord.Res_Khalti_Message = msgresult;
//////                                            // resUpdateRecord.Res_Khalti_Message = msgresult;
//////                                            resUpdateRecord.Res_Khalti_Id = resDeposit.RefferalsId;
//////                                            resUpdateRecord.Res_Khalti_State = AddDepositOrders.DepositStatus.Pending.ToString(); //AddDepositOrders.DepositStatus.Refund.ToString();
//////                                            //resUpdateRecord.Res_Khalti_State = AddDepositOrders.DepositStatus.Refund.ToString();

//////                                            resUpdateRecord.Res_Khalti_Status = false;
//////                                            resUpdateRecord.Res_Output = JsonConvert.SerializeObject(cres);
//////                                            resUpdateRecord.Res_Khalti_Output = JsonConvert.SerializeObject(res);
//////                                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
//////                                        }
//////                                        response.StatusCode = HttpStatusCode.BadRequest;
//////                                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//////                                    }
//////                                    return response;
//////                                }
//////                                else
//////                                {
//////                                    Common.AddLogs("Deposit Order Not Found", false, Convert.ToInt32(AddLog.LogType.Credit_Card_Payment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, user.PlatForm, user.DeviceCode);
//////                                    cres = CommonApiMethod.ReturnBadRequestMessage("Deposit Order Not Found");
//////                                    response.StatusCode = HttpStatusCode.BadRequest;
//////                                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//////                                    return response;
//////                                }
//////                            }
//////                            else
//////                            {
//////                                cres = CommonApiMethod.ReturnBadRequestMessage(transactionresult);

//////                                AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
//////                                GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
//////                                inobjApiResponse.Id = resVendor_API_Requests.Id;
//////                                AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
//////                                if (resUpdateRecord != null && resUpdateRecord.Id != 0 && resVendor_API_Requests.Id != 0)
//////                                {
//////                                    resUpdateRecord.Res_Khalti_State = AddDepositOrders.DepositStatus.Failed.ToString();
//////                                    resUpdateRecord.Res_Khalti_Status = false;
//////                                    resUpdateRecord.Res_Output = JsonConvert.SerializeObject(cres);
//////                                    RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
//////                                }
//////                                response.StatusCode = HttpStatusCode.BadRequest;
//////                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//////                                Common.AddLogs(cres.Message, false, Convert.ToInt32(AddLog.LogType.Credit_Card_Payment), user.MemberId, resuser.FirstName + " " + resuser.LastName, true, user.PlatForm, user.DeviceCode);

//////                                return response;
//////                            }
//////                        }
//////                        else
//////                        {
//////                            Common.AddLogs("Deposit Order Not Created", false, Convert.ToInt32(AddLog.LogType.Credit_Card_Payment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, user.PlatForm, user.DeviceCode);
//////                            cres = CommonApiMethod.ReturnBadRequestMessage("Deposit Order Not Created");
//////                            response.StatusCode = HttpStatusCode.BadRequest;
//////                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
//////                            return response;
//////                        }
//////                    }

//////                }
//////            }
//////            catch (DbEntityValidationException e)
//////            {
//////                foreach (var eve in e.EntityValidationErrors)
//////                {
//////                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//////                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
//////                    foreach (var ve in eve.ValidationErrors)
//////                    {
//////                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//////                            ve.PropertyName, ve.ErrorMessage);
//////                    }
//////                }
//////                log.Error($"{System.DateTime.Now.ToString()} CreditCardPayment {e.ToString()} " + Environment.NewLine);
//////                throw;
//////            }
//////            catch (Exception ex)
//////            {
//////                log.Error($"{System.DateTime.Now.ToString()} CreditCardPayment {ex.ToString()} " + Environment.NewLine);
//////                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
//////            }

//////        }

//////        private async Task<String> getRawPostData()
//////        {
//////            using (var contentStream = await this.Request.Content.ReadAsStreamAsync())
//////            {
//////                contentStream.Seek(0, SeekOrigin.Begin);
//////                using (var sr = new StreamReader(contentStream))
//////                {
//////                    return sr.ReadToEnd();
//////                }
//////            }
//////        }

//////        public static string WalletDeduct(AddCouponsScratched resCoupon, AddCalculateServiceChargeAndCashback objOut, AddUserLoginWithPin resuser, decimal totalamount, string authenticationtoken, string TransactionId, decimal amount, decimal servicecharge, string cardnumber, string code, AddVendor_API_Requests resVendor_API_Requests, string PlatForm, string DeviceCode, Int32 VendorApiType, ref string uniqueTransactionId, Int32 WalletType = 1)
//////        {
//////            string result = "";
//////            try
//////            {
//////                decimal CouponDeduct = 0;
//////                decimal WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resuser.TotalAmount) - Convert.ToDecimal(totalamount));
//////                decimal RewardPointBalance = Convert.ToDecimal(resuser.TotalRewardPoints);
//////                if (WalletType == 0 || WalletType == ((int)WalletTransactions.WalletTypes.Wallet))
//////                {
//////                    WalletType = ((int)WalletTransactions.WalletTypes.Wallet);
//////                    CouponDeduct = CouponDeduct + Convert.ToDecimal((Convert.ToDecimal(amount) * resCoupon.CouponPercentage) / 100);
//////                    WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resuser.TotalAmount) - Convert.ToDecimal(totalamount) - CouponDeduct);
//////                    RewardPointBalance = Convert.ToDecimal(resuser.TotalRewardPoints);
//////                }
//////                else if (WalletType == ((int)WalletTransactions.WalletTypes.MPCoins))
//////                {
//////                    CouponDeduct = CouponDeduct + Convert.ToDecimal((Convert.ToDecimal(amount) * resCoupon.CouponPercentage) / 100);
//////                    WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resuser.TotalAmount) - ((Convert.ToDecimal(totalamount) - objOut.MPCoinsDebit - CouponDeduct)));
//////                    RewardPointBalance = Convert.ToDecimal(Convert.ToDecimal(resuser.TotalRewardPoints) - objOut.MPCoinsDebit);
//////                }
//////                WalletTransactions res_transaction = new WalletTransactions();
//////                res_transaction.VendorTransactionId = TransactionId;
//////                if (!res_transaction.GetRecordCheckExists())
//////                {
//////                    res_transaction.MemberId = Convert.ToInt64(resuser.MemberId);
//////                    res_transaction.ContactNumber = resuser.ContactNumber;
//////                    res_transaction.MemberName = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
//////                    if (WalletType == ((int)WalletTransactions.WalletTypes.MPCoins))
//////                    {
//////                        res_transaction.Amount = Convert.ToDecimal((amount + servicecharge)) - objOut.MPCoinsDebit;
//////                    }
//////                    else
//////                    {
//////                        res_transaction.Amount = Convert.ToDecimal((amount + servicecharge)) - Convert.ToDecimal(CouponDeduct);
//////                    }
//////                    res_transaction.UpdateBy = Convert.ToInt64(resuser.MemberId);
//////                    res_transaction.UpdateByName = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
//////                    res_transaction.CurrentBalance = WalletBalance;
//////                    res_transaction.RewardPointBalance = RewardPointBalance;
//////                    res_transaction.CreatedBy = Common.GetCreatedById(authenticationtoken);
//////                    res_transaction.CreatedByName = Common.GetCreatedByName(authenticationtoken);
//////                    res_transaction.TransactionUniqueId = new CommonHelpers().GenerateUniqueId();
//////                    uniqueTransactionId = res_transaction.TransactionUniqueId;
//////                    res_transaction.VendorTransactionId = new CommonHelpers().GenerateUniqueId();
//////                    res_transaction.Reference = TransactionId;
//////                    //res_transaction.BatchTransactionId = res.cipsBatchResponse.batchId;
//////                    //res_transaction.TxnInstructionId = res.cipsTxnResponseList[0].instructionId;
//////                    res_transaction.Remarks = "Credit Card Payment Successfully Deposit";
//////                    res_transaction.GatewayStatus = WalletTransactions.Statuses.Pending.ToString();
//////                    res_transaction.ResponseCode = "";
//////                    res_transaction.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Credit_Card_Payment;
//////                    res_transaction.Description = "Credit Card Payment Successfully Deposit";
//////                    res_transaction.Status = (int)WalletTransactions.Statuses.Pending;
//////                    res_transaction.IsApprovedByAdmin = true;
//////                    res_transaction.IsActive = true;
//////                    res_transaction.Sign = Convert.ToInt16(WalletTransactions.Signs.Debit);
//////                    res_transaction.RecieverName = Common.ConnectIPs_AccountNumber;
//////                    res_transaction.TransferType = (int)WalletTransactions.TransferTypes.Sender;
//////                    res_transaction.RecieverAccountNo = Common.ConnectIPs_AccountNumber;
//////                    res_transaction.RecieverBankCode = Common.ConnectIps_BankId;
//////                    res_transaction.RecieverBranch = Common.ConnectIPs_BranchName;
//////                    res_transaction.CardNumber = cardnumber;
//////                    res_transaction.CardType = code;
//////                    res_transaction.ServiceCharge = (objOut.ServiceCharge + servicecharge);
//////                    res_transaction.NetAmount = totalamount;
//////                    res_transaction.Purpose = "Credit Card Payment";
//////                    res_transaction.Platform = PlatForm;
//////                    res_transaction.WalletType = WalletType;
//////                    res_transaction.VendorType = (int)VendorApi_CommonHelper.VendorTypes.Prabhu;

//////                    res_transaction.RecieverBankName = Common.ConnectIPs_BankName;
//////                    res_transaction.RecieverBranchName = Common.ConnectIPs_BranchName;

//////                    if ((WalletType == ((int)WalletTransactions.WalletTypes.Wallet) || WalletType == ((int)WalletTransactions.WalletTypes.FonePay)))
//////                    {
//////                        res_transaction.TransactionAmount = objOut.Amount;
//////                        res_transaction.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
//////                        res_transaction.CouponCode = resCoupon.CouponCode;
//////                        res_transaction.CouponDiscount = CouponDeduct;
//////                    }
//////                    else if (WalletType == ((int)WalletTransactions.WalletTypes.MPCoins))
//////                    {
//////                        res_transaction.TransactionAmount = objOut.Amount;
//////                        res_transaction.MPCoinsDebit = objOut.MPCoinsDebit;
//////                        res_transaction.RewardPointBalance = (resuser.TotalRewardPoints) - objOut.MPCoinsDebit;
//////                        res_transaction.WalletType = (int)WalletTransactions.WalletTypes.MPCoins;
//////                        res_transaction.CouponDiscount = CouponDeduct;
//////                    }
//////                    if (res_transaction.Add())
//////                    {
//////                        Common.AddLogs("Credit Card Payment Successfully Deposit", false, Convert.ToInt32(AddLog.LogType.Credit_Card_Payment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, PlatForm, DeviceCode);
//////                        result = "success";
//////                    }
//////                    else
//////                    {
//////                        Common.AddLogs("Something Went Wrong Payment Not Sent", false, Convert.ToInt32(AddLog.LogType.Credit_Card_Payment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, PlatForm, DeviceCode);
//////                        result = "Something Went Wrong Payment Not Sent";
//////                    }
//////                }
//////                else
//////                {

//////                    Common.AddLogs("Transaction Sent Already", false, Convert.ToInt32(AddLog.LogType.Credit_Card_Payment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, PlatForm, DeviceCode);
//////                    result = "Transaction Sent Already";
//////                }
//////            }
//////            catch (Exception ex)
//////            {
//////                result = ex.Message;
//////            }
//////            return result;
//////        }

//////        public static string WalletRefund(string TransactionId, AddUserLoginWithPin resuser, decimal totalamount, string authenticationtoken, string Platform, string DeviceCode)
//////        {
//////            string result = "";
//////            try
//////            {
//////                WalletTransactions res_transaction = new WalletTransactions();
//////                res_transaction.Reference = TransactionId;
//////                if (res_transaction.GetRecord())
//////                {
//////                    WalletTransactions newtra = new WalletTransactions();
//////                    decimal WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resuser.TotalAmount));
//////                    newtra.MemberId = Convert.ToInt64(resuser.MemberId);
//////                    newtra.ContactNumber = resuser.ContactNumber;
//////                    newtra.MemberName = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
//////                    newtra.Amount = (res_transaction.NetAmount);
//////                    newtra.UpdateBy = Convert.ToInt64(resuser.MemberId);
//////                    newtra.UpdateByName = resuser.FirstName + " " + resuser.MiddleName + " " + resuser.LastName;
//////                    newtra.CurrentBalance = WalletBalance;
//////                    newtra.CreatedBy = Common.GetCreatedById(authenticationtoken);
//////                    newtra.CreatedByName = Common.GetCreatedByName(authenticationtoken);
//////                    newtra.TransactionUniqueId = new CommonHelpers().GenerateUniqueId();
//////                    newtra.VendorTransactionId = new CommonHelpers().GenerateUniqueId();
//////                    newtra.Reference = TransactionId;
//////                    newtra.ParentTransactionId = res_transaction.TransactionUniqueId;
//////                    newtra.Remarks = "Credit Card Payment Successfully Refunded for Transactino ID: " + res_transaction.TransactionUniqueId;
//////                    newtra.GatewayStatus = WalletTransactions.Statuses.Success.ToString();
//////                    newtra.ResponseCode = "";
//////                    newtra.Type = (int)VendorApi_CommonHelper.KhaltiAPIName.Credit_Card_Payment;
//////                    newtra.Description = "Credit Card Payment Successfully Refunded";
//////                    newtra.Status = (int)WalletTransactions.Statuses.Refund;
//////                    newtra.IsApprovedByAdmin = true;
//////                    newtra.IsActive = true;
//////                    newtra.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
//////                    newtra.RecieverName = Common.ConnectIPs_AccountNumber;
//////                    newtra.TransferType = (int)WalletTransactions.TransferTypes.Sender;
//////                    newtra.RecieverAccountNo = Common.ConnectIPs_AccountNumber;
//////                    newtra.RecieverBankCode = Common.ConnectIps_BankId;
//////                    newtra.RecieverBranch = Common.ConnectIPs_BranchName;
//////                    newtra.CardNumber = res_transaction.CardNumber;
//////                    newtra.CardType = res_transaction.CardType;
//////                    newtra.ServiceCharge = (res_transaction.ServiceCharge);
//////                    newtra.NetAmount = totalamount;
//////                    newtra.Purpose = "Credit Card Payment Refund";
//////                    newtra.Platform = Platform;
//////                    newtra.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
//////                    newtra.VendorType = (int)VendorApi_CommonHelper.VendorTypes.MyPay;


//////                    newtra.Amount = Convert.ToDecimal(res_transaction.NetAmount);
//////                    newtra.RewardPoint = res_transaction.RewardPoint;
//////                    newtra.RewardPointBalance = res_transaction.RewardPointBalance;
//////                    if (res_transaction.WalletType == ((int)WalletTransactions.WalletTypes.MPCoins))
//////                    {
//////                        newtra.Amount = Convert.ToDecimal(res_transaction.Amount) + Convert.ToDecimal(res_transaction.ServiceCharge);
//////                        newtra.RewardPoint = res_transaction.RewardPoint;
//////                        newtra.MPCoinsDebit = res_transaction.MPCoinsDebit;
//////                        newtra.RewardPointBalance = res_transaction.RewardPointBalance;
//////                        WalletBalance = Convert.ToDecimal(Convert.ToDecimal(resuser.TotalAmount));
//////                    }

//////                    if (newtra.Add())
//////                    {
//////                        res_transaction.Remarks = "Credit Card Payment Failed";
//////                        res_transaction.Status = (int)WalletTransactions.Statuses.Failed;
//////                        res_transaction.GatewayStatus = WalletTransactions.Statuses.Failed.ToString();
//////                        if (res_transaction.Update())
//////                        {
//////                            result = "success";
//////                        }
//////                        else
//////                        {
//////                            result = "Refunded But Transaction Not Updated";
//////                        }
//////                    }
//////                    else
//////                    {
//////                        Common.AddLogs("Something Went Wrong Payment Not Sent", false, Convert.ToInt32(AddLog.LogType.Credit_Card_Payment), resuser.MemberId, resuser.FirstName + " " + resuser.LastName, true, Platform, DeviceCode);
//////                        result = "Something Went Wrong Payment Not Sent";
//////                    }
//////                }
//////                else
//////                {
//////                    result = "Transaction Not Found For Refund";
//////                }
//////            }
//////            catch (Exception ex)
//////            {
//////                result = ex.Message;
//////            }
//////            return result;
//////        }
//////    }
//////}