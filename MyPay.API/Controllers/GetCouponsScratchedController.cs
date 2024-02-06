using log4net;
using MyPay.API.Models;
using MyPay.API.Models.Request;
using MyPay.API.Models.Response;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.GenericCoupons;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class GetCouponsScratchedController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(GetCouponsScratchedController));
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/getScratchedCoupons")]
        public HttpResponseMessage GetScratchedCoupons(Req_CouponsScratched user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside GetScratchedCoupons" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            List<AddCouponsScratched> list = new List<AddCouponsScratched>();
            Res_CouponsScratched result = new Res_CouponsScratched();
            var response = Request.CreateResponse<Res_CouponsScratched>(System.Net.HttpStatusCode.BadRequest, result);

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
                        if (string.IsNullOrEmpty(user.ServiceId.ToString()) || user.ServiceId.ToString() == "0")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage("ServiceId Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        if (res != null && res.Id != 0)
                        {
                            list = new List<AddCouponsScratched>();
                            AddCouponsScratched outobject = new AddCouponsScratched();
                            GetCouponsScratched inobject = new GetCouponsScratched();
                            inobject.CheckDelete = 0;
                            inobject.CheckActive = 1;
                            inobject.GenderStatus = res.Gender;
                            inobject.KycStatus = res.IsKYCApproved;
                            inobject.IsScratched = 1;
                            //inobject.IsUsed = 0;
                            //inobject.IsExpired = 0;
                            inobject.CouponType = (int)AddCoupons.CouponTypeEnum.Coupon;
                            inobject.MemberId = Convert.ToInt64(user.MemberId);
                            inobject.ServiceId = Convert.ToInt32(user.ServiceId);

                            inobject.Take = Convert.ToInt32(user.Take);
                            inobject.Skip = Convert.ToInt32(user.Skip) * Convert.ToInt32(user.Take);
                            list = RepCRUD<GetCouponsScratched, AddCouponsScratched>.GetRecordList(MyPay.Models.Common.Common.StoreProcedures.sp_CouponsScratched_Get, inobject, outobject);
                            result.status = true;
                            result.data = list;
                            result.Message = "Success";
                            result.ReponseCode = 1;
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("User Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} GetScratchedCoupons completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} GetScratchedCoupons {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/getCouponsAll")]
        public HttpResponseMessage GetCouponsAll(Req_CouponsScratchedAll user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside GetScratchedCoupons" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            List<AddCouponsScratched> list = new List<AddCouponsScratched>();
            Res_CouponsScratched result = new Res_CouponsScratched();
            var response = Request.CreateResponse<Res_CouponsScratched>(System.Net.HttpStatusCode.BadRequest, result);

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
                        if (res != null && res.Id != 0)
                        {
                            list = new List<AddCouponsScratched>();
                            AddCouponsScratched outobject = new AddCouponsScratched();
                            GetCouponsScratched inobject = new GetCouponsScratched();
                            inobject.CheckDelete = 0;
                            inobject.CheckActive = 1;
                            //inobject.IsUsed = 0;
                            //inobject.IsExpired = 0;
                            inobject.GenderStatus = res.Gender;
                            inobject.KycStatus = res.IsKYCApproved;
                            inobject.MemberId = Convert.ToInt64(user.MemberId);
                            inobject.Take = Convert.ToInt32(user.Take);
                            inobject.Skip = Convert.ToInt32(user.Skip) * Convert.ToInt32(user.Take);
                            list = RepCRUD<GetCouponsScratched, AddCouponsScratched>.GetRecordList(MyPay.Models.Common.Common.StoreProcedures.sp_CouponsScratched_Get, inobject, outobject);
                            result.status = true;
                            result.data = list;
                            result.Message = "Success";
                            result.ReponseCode = 1;
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("User Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} GetScratchedCoupons completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} GetScratchedCoupons {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/ScratchCoupons")]
        public HttpResponseMessage ScratchCoupons(Req_CouponsScratchNow user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside GetScratchedCoupons" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            List<AddCouponsScratched> list = new List<AddCouponsScratched>();
            Res_CouponsScratchNow result = new Res_CouponsScratchNow();
            var response = Request.CreateResponse<Res_CouponsScratchNow>(System.Net.HttpStatusCode.BadRequest, result);

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
                        string authenticationToken = Request.Headers.Authorization.Parameter;
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
                        if (string.IsNullOrEmpty(user.CouponCode) || user.CouponCode == "0")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage("CouponCode Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        if (res != null && res.Id != 0)
                        {
                            AddCouponsScratched obj = new AddCouponsScratched();
                            AddCouponsScratched outobject = new AddCouponsScratched();
                            GetCouponsScratched inobject = new GetCouponsScratched();
                            inobject.CheckDelete = 0;
                            inobject.CheckActive = 1;
                            inobject.IsUsed = 0;
                            //inobject.IsExpired = 0;
                            inobject.IsScratched = 0;
                            inobject.Id = user.Id;
                            inobject.CouponCode = user.CouponCode;
                            inobject.MemberId = Convert.ToInt64(user.MemberId);
                            obj = RepCRUD<GetCouponsScratched, AddCouponsScratched>.GetRecord(MyPay.Models.Common.Common.StoreProcedures.sp_CouponsScratched_Get, inobject, outobject);
                            if (obj.Id > 0)
                            {
                                obj.IsScratched = 1;
                                if (obj.CouponType != (int)AddCoupons.CouponTypeEnum.Coupon)
                                {
                                    obj.IsUsed = 1;
                                }
                                bool IsUpdated = RepCRUD<AddCouponsScratched, GetCouponsScratched>.Update(obj, "couponsscratched");
                                if (IsUpdated)
                                {
                                    if (obj.CouponAmount > 0 && obj.CouponType == (int)AddCoupons.CouponTypeEnum.Wallet)
                                    {
                                        Guid referenceGuid = Guid.NewGuid();
                                        string TransactionUniqueID = new CommonHelpers().GenerateUniqueId();
                                        string ReferenceNo = referenceGuid.ToString();
                                        WalletTransactions res_transaction = new WalletTransactions();
                                        string CouponAmount = Convert.ToString(obj.CouponAmount);
                                        int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.Coupons_Cashback;
                                        // DISTRIBUTE COUPON AMOUNT
                                        decimal currentBalance = res.TotalAmount + Convert.ToDecimal(CouponAmount);
                                        res_transaction.MemberId = res.MemberId;
                                        res_transaction.ContactNumber = res.ContactNumber;
                                        res_transaction.MemberName = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                                        res_transaction.Amount = Convert.ToDecimal(CouponAmount);
                                        res_transaction.VendorTransactionId = TransactionUniqueID;
                                        res_transaction.ParentTransactionId = String.Empty; // THIS FIELD IS KEPT EMPTY FOR NOW -- IT SHOULD BE PARENT TRANSACTION ID AGAINST CASHBACK.
                                        res_transaction.CurrentBalance = currentBalance;
                                        res_transaction.CreatedBy = Common.CreatedBy;
                                        res_transaction.CreatedByName = Common.CreatedByName;
                                        res_transaction.TransactionUniqueId = TransactionUniqueID;
                                        res_transaction.Remarks = $"Coupon Code is Redeemed: {user.CouponCode}. Wallet Credit Rs. {obj.CouponAmount}";
                                        res_transaction.Type = VendorAPIType;
                                        res_transaction.Description = $"Coupon Code is Redeemed: {user.CouponCode}. Wallet Credit Rs. {obj.CouponAmount}";
                                        res_transaction.Status = (int)WalletTransactions.Statuses.Success;
                                        res_transaction.GatewayStatus = WalletTransactions.Statuses.Success.ToString();
                                        res_transaction.Reference = ReferenceNo;
                                        res_transaction.IsApprovedByAdmin = true;
                                        res_transaction.IsActive = true;
                                        res_transaction.CashBack = 0;
                                        res_transaction.NetAmount = res_transaction.Amount;
                                        res_transaction.TransferType = (int)WalletTransactions.TransferTypes.Reciever;
                                        res_transaction.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
                                        res_transaction.DeviceCode = user.DeviceCode;
                                        res_transaction.Platform = user.PlatForm;
                                        res_transaction.WalletType = (int)WalletTransactions.WalletTypes.Wallet;
                                        res_transaction.VendorType = (int)VendorApi_CommonHelper.VendorTypes.MyPay;
                                        res_transaction.CouponCode = user.CouponCode;
                                        res_transaction.CouponDiscount = Convert.ToDecimal(CouponAmount);
                                        bool CouponDiscountUpdate = false;
                                        // DISTRIBUTE COUPON
                                        CouponDiscountUpdate = res_transaction.Add();
                                        if (CouponDiscountUpdate)
                                        {
                                            // SEND NOTIFICATION  
                                            string Title = "Coupon Redeem";
                                            string Message = $"Your Coupon Code is Redeemed: {user.CouponCode} .Wallet Credit Rs. {obj.CouponAmount}. ";
                                            Common.SendNotification(authenticationToken, VendorAPIType, res.MemberId, Title, Message);
                                        }
                                    }
                                    else if (obj.CouponAmount > 0 && obj.CouponType == (int)AddCoupons.CouponTypeEnum.MPCoins)
                                    {
                                        Guid referenceGuid = Guid.NewGuid();
                                        string TransactionUniqueID = new CommonHelpers().GenerateUniqueId();
                                        string ReferenceNo = referenceGuid.ToString();
                                        RewardPointTransactions res_transaction = new RewardPointTransactions();
                                        string CouponAmount = Convert.ToString(obj.CouponAmount);
                                        int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.Coupons_Cashback;
                                        // DISTRIBUTE COUPON AMOUNT
                                        decimal currentBalance = res.TotalAmount + Convert.ToDecimal(CouponAmount);
                                        res_transaction.MemberId = res.MemberId;
                                        res_transaction.MemberName = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                                        res_transaction.Amount = Convert.ToDecimal(CouponAmount);
                                        res_transaction.VendorTransactionId = TransactionUniqueID;
                                        res_transaction.ParentTransactionId = String.Empty; // THIS FIELD IS KEPT EMPTY FOR NOW -- IT SHOULD BE PARENT TRANSACTION ID AGAINST CASHBACK.
                                        res_transaction.CurrentBalance = currentBalance;
                                        res_transaction.CreatedBy = Common.CreatedBy;
                                        res_transaction.CreatedByName = Common.CreatedByName;
                                        res_transaction.TransactionUniqueId = TransactionUniqueID;
                                        res_transaction.Remarks = $"Coupon Code is Redeemed: {user.CouponCode}";
                                        res_transaction.Type = VendorAPIType;
                                        res_transaction.Description = $"Coupon Code is Redeemed: {user.CouponCode}";
                                        res_transaction.Status = (int)WalletTransactions.Statuses.Success;
                                        res_transaction.Reference = ReferenceNo;
                                        res_transaction.IsApprovedByAdmin = true;
                                        res_transaction.IsActive = true;
                                        res_transaction.Sign = Convert.ToInt16(WalletTransactions.Signs.Credit);
                                        bool CouponDiscountUpdate = false;
                                        // DISTRIBUTE COUPON
                                        CouponDiscountUpdate = res_transaction.Add();
                                        if (CouponDiscountUpdate)
                                        {
                                            // SEND NOTIFICATION  
                                            string Title = "Coupon Redeem";
                                            string Message = $"Your Coupon Code is Redeemed: {user.CouponCode}. MyPay Coins Credit  {obj.CouponAmount}. ";
                                            Common.SendNotification(authenticationToken, VendorAPIType, res.MemberId, Title, Message);
                                        }
                                    }
                                    result.data = obj;
                                    result.status = true;
                                    result.Message = "Success";
                                    result.ReponseCode = 1;
                                    response.StatusCode = HttpStatusCode.Accepted;
                                    response = Request.CreateResponse<Res_CouponsScratchNow>(System.Net.HttpStatusCode.OK, result);

                                }
                                else
                                {
                                    cres = CommonApiMethod.ReturnBadRequestMessage("Coupon Scratch Failed. Please try again later.");
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                    return response;
                                }
                            }
                            else
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage("Coupon Not Found. Please try again later.");
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                return response;
                            }
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("User Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} GetScratchedCoupons completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} GetScratchedCoupons {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/getCouponsApply")]
        public HttpResponseMessage GetCouponsApply(Req_CouponsScratchedApply user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside GetScratchedCoupons" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            List<AddCouponsScratched> list = new List<AddCouponsScratched>();
            Res_CouponsScratched result = new Res_CouponsScratched();
            var response = Request.CreateResponse<Res_CouponsScratched>(System.Net.HttpStatusCode.BadRequest, result);

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
                        if (string.IsNullOrEmpty(user.ServiceId) || user.ServiceId == "0")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage("ServiceId Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        if (string.IsNullOrEmpty(user.CouponCode) || user.CouponCode == "0")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage("CouponCode Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        if (res != null && res.Id != 0)
                        {
                            list = new List<AddCouponsScratched>();
                            AddCouponsScratched outobject = new AddCouponsScratched();
                            GetCouponsScratched inobject = new GetCouponsScratched();
                            inobject.CheckDelete = 0;
                            inobject.CheckActive = 1;
                            inobject.GenderStatus = res.Gender;
                            inobject.KycStatus = res.IsKYCApproved;
                            inobject.MemberId = Convert.ToInt64(user.MemberId);
                            inobject.CouponCode = user.CouponCode;
                            inobject.ServiceId = Convert.ToInt32(user.ServiceId);
                            //inobject.IsExpired = 0;
                            //inobject.IsUsed = 0;
                            inobject.Take = user.Take;
                            inobject.Skip = Convert.ToInt32(user.Skip) * Convert.ToInt32(user.Take);
                            inobject.CouponType = (int)AddCoupons.CouponTypeEnum.Coupon;
                            list = RepCRUD<GetCouponsScratched, AddCouponsScratched>.GetRecordList(MyPay.Models.Common.Common.StoreProcedures.sp_CouponsScratched_Get, inobject, outobject);
                            if (list.Count > 0)
                            {
                                result.status = true;
                                result.data = list;
                                result.Message = "Success";
                                result.ReponseCode = 1;
                                response.StatusCode = HttpStatusCode.Accepted;
                                response = Request.CreateResponse<Res_CouponsScratched>(System.Net.HttpStatusCode.OK, result);
                            }
                            else
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage("Coupon Not Found");
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                return response;
                            }
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("User Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} GetScratchedCoupons completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} GetScratchedCoupons {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/v2/getCouponsApply")]
        public HttpResponseMessage GetCouponsApplyV2(Req_GenericCoupon user)
        {

            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            ILog log2 = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

           // string UserInput = getRawPostData().Result;


            Res_CouponsScratched result = new Res_CouponsScratched();
            var response = Request.CreateResponse<Res_CouponsScratched>(System.Net.HttpStatusCode.BadRequest, result);
            CommonResponse cres = new CommonResponse();
            var userInput = getRawPostData().Result;

            switch (user.CouponType)
            {
                case CouponTypes.EventPromoCode:

                    ValidateCouponResponse eventCouponResp = new ValidateCouponResponse();
                    var newEventsCouponResponse = Request.CreateResponse<ValidateCouponResponse>(System.Net.HttpStatusCode.BadRequest, eventCouponResp);
                    
                    if (string.IsNullOrEmpty(user.CustomerEmail))
                    {
                        user.CustomerEmail = user.MemberId + "@myPayMobile.com";
                    }


                    if (Request.Headers.Authorization == null)
                    {
                        string results = "Un-Authorized Request";
                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return newEventsCouponResponse;
                    }
                    else
                    {
                        string md5hash = Common.getHashMD5(userInput); //  Common.CheckHash(user);

                        string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                        if (results != "Success")
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(results);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return newEventsCouponResponse;
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
                                res = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, "", user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
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

                            if (string.IsNullOrEmpty(user.EventId.ToString()) || user.EventId == 0)
                            {
                                CommonResponse cres1 = new CommonResponse();
                                cres1 = CommonApiMethod.ReturnBadRequestMessage("Event ID Not Found");
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                                return response;
                            }
                            if (string.IsNullOrEmpty(user.CouponCode) || user.CouponCode == "0")
                            {
                                CommonResponse cres1 = new CommonResponse();
                                cres1 = CommonApiMethod.ReturnBadRequestMessage("CouponCode Not Found");
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                                return response;
                            }
                            if (string.IsNullOrEmpty(user.CustomerEmail))
                            {
                                CommonResponse cres1 = new CommonResponse();
                                cres1 = CommonApiMethod.ReturnBadRequestMessage("Customer email Not Found");
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                                return response;
                            }

                            //if (string.IsNullOrEmpty(user.MerchantCode) || user.MerchantCode == "0")
                            //{
                            //    CommonResponse cres1 = new CommonResponse();
                            //    cres1 = CommonApiMethod.ReturnBadRequestMessage("Merchant code Not Found");
                            //    response.StatusCode = HttpStatusCode.BadRequest;
                            //    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            //    return response;
                            //}

                            if (string.IsNullOrEmpty(user.Amount.ToString()) || user.Amount == 0)
                            {
                                CommonResponse cres1 = new CommonResponse();
                                cres1 = CommonApiMethod.ReturnBadRequestMessage("CouponCode Not Found");
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                                return response;
                            }

                            if (res != null && res.Id != 0)
                            {
                                string EventsAPIURL = "/clientapi/validate-promocode/";
                                string ApiResponse = string.Empty;


                                string authenticationToken = Request.Headers.Authorization.Parameter;


                                

                                Int64 memId = Convert.ToInt64(user.MemberId);
                                int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.MyPay_Events;
                                int Type = 0;
                                AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                                string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorAPIType).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                                objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse(EventsAPIURL, user.ReferenceNo, res.MemberId, res.FirstName + " " + res.LastName, "", authenticationToken, userInput, user.DeviceCode, user.PlatForm, VendorAPIType, "", "", "", ((int)VendorApi_CommonHelper.VendorTypes.MyPay).ToString());

                                //GetVendor_API_Events_Ticket objRes = new GetVendor_API_Events_Ticket();
                                //Res_Vendor_API_Events_Requests objMaxTickets = new Res_Vendor_API_Events_Requests();
                                //if (user.NoOfTicket > objMaxTickets.MaxTicketsAllowed)
                                //{
                                //    cres = CommonApiMethod.ReturnBadRequestMessage("Maximum number of ticket allowed is 25.");
                                //    response.StatusCode = HttpStatusCode.BadRequest;
                                //    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                //    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                                //}
                                //else
                                //{
                                var objRes = new ValidateCouponResponse();
                                string msg = RepEvents.Request_Coupon_Verification( user.CustomerEmail, user.EventId, user.Amount, user.CouponCode, ref objRes, ref objVendor_API_Requests, CouponTypes.EventPromoCode, user.MerchantCode);



                                //RequestServiceGroup_Event_Ticket(ref objVendor_API_Requests, user.MerchantCode, user.CustomerName, user.CustomerMobile, user.CustomerEmail, user.EventId, user.TicketCategoryId, user.TicketCategoryName,
                                //user.EventDate, user.TicketRate, user.NoOfTicket, user.TotalPrice, user.PaymentMethodId, user.DeviceId, ref objRes);
                                if (objRes.Success)
                                {
                                    // return objRes;
                                    //newEventsCouponResponse.
                                    //response = Request.CreateResponse<Res_CouponsScratched>(System.Net.HttpStatusCode.OK, result);
                                    log.Info("Coupon Validation Message" + objRes.Message);
                                    objRes.status = true;
                                    newEventsCouponResponse = Request.CreateResponse<ValidateCouponResponse>(System.Net.HttpStatusCode.OK, objRes);
                                    return newEventsCouponResponse;
                                }
                                else
                                {
                                    cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    cres.Message = objRes.Errors[0];
                                    cres.ReponseCode = 3;
                                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(msg);
                                }

                                //AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                                //GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                                //inobjApiResponse.Id = objVendor_API_Requests.Id;
                                //AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                                //if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id > 0)
                                //{
                                //    resUpdateRecord.Res_Output = ApiResponse;
                                //    RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                                //}
                            }

                        }
                    }
                    // return response; // newEventsCouponResponse;
                    return response;
                    break;
                case CouponTypes.VotingPromoCode:
                    ValidateCouponResponse votingCouponResp = new ValidateCouponResponse();
                    var newVotingCouponResponse = Request.CreateResponse<ValidateCouponResponse>(System.Net.HttpStatusCode.BadRequest, votingCouponResp);


                    if (string.IsNullOrEmpty(user.CustomerEmail))
                    {
                        user.CustomerEmail = user.MemberId + "@myPayMobile.com";
                    }

                    log2.Info("Created user email" + user.CustomerEmail);

                    if (Request.Headers.Authorization == null)
                    {
                        string results = "Un-Authorized Request";
                        cres = CommonApiMethod.ReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        log2.Info("Authorization failed");
                        return newVotingCouponResponse;
                    }
                    else
                    {
                        string md5hash = Common.getHashMD5(userInput); // Common.CheckHash(user);

                        string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey, userInput);
                        if (results != "Success")
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage(results);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            log2.Info("check API Token FAILED");
                            return response;
                        }
                        else
                        {

                            log2.Info("check API Token PASSED");

                            string CommonResult = "";
                            AddUserLoginWithPin res = new AddUserLoginWithPin();
                            AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                            if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                            {
                                Int64 memId = Convert.ToInt64(user.MemberId);
                                int VendorAPIType = 0;
                                int Type = 0;
                                res = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, "", user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
                                if (CommonResult.ToLower() != "success")
                                {
                                    CommonResponse cres1 = new CommonResponse();
                                    cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                                    log2.Info("check User detailed FAILED");

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

                            if (string.IsNullOrEmpty(user.EventId.ToString()) || user.EventId == 0)
                            {
                                CommonResponse cres1 = new CommonResponse();
                                cres1 = CommonApiMethod.ReturnBadRequestMessage("Event ID Not Found");
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                                return response;
                            }
                            if (string.IsNullOrEmpty(user.CouponCode) || user.CouponCode == "0")
                            {
                                CommonResponse cres1 = new CommonResponse();
                                cres1 = CommonApiMethod.ReturnBadRequestMessage("CouponCode Not Found");
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                                return response;
                            }
                            if (string.IsNullOrEmpty(user.CustomerEmail))
                            {
                                CommonResponse cres1 = new CommonResponse();
                                cres1 = CommonApiMethod.ReturnBadRequestMessage("Customer email Not Found");
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                                return response;
                            }

                            if (string.IsNullOrEmpty(user.Amount.ToString()) || user.Amount == 0)
                            {
                                CommonResponse cres1 = new CommonResponse();
                                cres1 = CommonApiMethod.ReturnBadRequestMessage("CouponCode Not Found");
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                                return response;
                            }

                            if (res != null && res.Id != 0)
                            {
                                string EventsAPIURL = "/clientapi/validate-promocode/";
                                string ApiResponse = string.Empty;


                                string authenticationToken = Request.Headers.Authorization.Parameter;


                               // string UserInput = getRawPostData().Result;
                                log2.Info("User coupon request: " + userInput);


                                Int64 memId = Convert.ToInt64(user.MemberId);
                                int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.International_Voting;
                                int Type = 0;
                                AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                                string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorAPIType).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                                objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse(EventsAPIURL, user.ReferenceNo, res.MemberId, res.FirstName + " " + res.LastName, "", authenticationToken, userInput, user.DeviceCode, user.PlatForm, VendorAPIType, "", "", "", ((int)VendorApi_CommonHelper.VendorTypes.MyPay).ToString());

                                //GetVendor_API_Events_Ticket objRes = new GetVendor_API_Events_Ticket();
                                //Res_Vendor_API_Events_Requests objMaxTickets = new Res_Vendor_API_Events_Requests();
                                //if (user.NoOfTicket > objMaxTickets.MaxTicketsAllowed)
                                //{
                                //    cres = CommonApiMethod.ReturnBadRequestMessage("Maximum number of ticket allowed is 25.");
                                //    response.StatusCode = HttpStatusCode.BadRequest;
                                //    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                //    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                                //}
                                //else
                                //{

                                log2.Info("Requesting coupon Validation");

                                var objRes = new ValidateCouponResponse();

                                string msg = RepEvents.Request_Coupon_Verification(user.CustomerEmail, user.EventId, user.Amount, user.CouponCode, ref objRes, ref objVendor_API_Requests, CouponTypes.VotingPromoCode);



                                //RequestServiceGroup_Event_Ticket(ref objVendor_API_Requests, user.MerchantCode, user.CustomerName, user.CustomerMobile, user.CustomerEmail, user.EventId, user.TicketCategoryId, user.TicketCategoryName,
                                //user.EventDate, user.TicketRate, user.NoOfTicket, user.TotalPrice, user.PaymentMethodId, user.DeviceId, ref objRes);
                                if (objRes.Success)
                                {
                                    // return objRes;
                                    //newEventsCouponResponse.
                                    //response = Request.CreateResponse<Res_CouponsScratched>(System.Net.HttpStatusCode.OK, result);
                                    log2.Info("Coupon is Valid"); 
                                    log2.Info("Discount Amount" + objRes.Data.DiscountAmt);
                                    log2.Info("Previous Amount" + objRes.Data.PreviousAmount);
                                    log2.Info("Net Payable Amount" + objRes.Data.netPayableAmtAfterCouponApplied);
                                    objRes.status = true;
                                    newEventsCouponResponse = Request.CreateResponse<ValidateCouponResponse>(System.Net.HttpStatusCode.OK, objRes);
                                    return newEventsCouponResponse;
                                }
                                else
                                {
                                    log2.Info("Coupon is Invalid");
                                   
                                    cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    cres.Message = objRes.Errors[0];
                                    cres.ReponseCode = 3;

                                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(msg);
                                }

                                //AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                                //GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                                //inobjApiResponse.Id = objVendor_API_Requests.Id;
                                //AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                                //if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id > 0)
                                //{
                                //    resUpdateRecord.Res_Output = ApiResponse;
                                //    RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                                //}
                            }

                        }
                    }
                    return response;

                    break;
                case CouponTypes.OtherPromoCode:
                default:
                    log.Info($"{System.DateTime.Now.ToString()} inside GetScratchedCoupons" + Environment.NewLine);

                    List<AddCouponsScratched> list = new List<AddCouponsScratched>();
                    //Res_CouponsScratched result = new Res_CouponsScratched();
                    //var response = Request.CreateResponse<Res_CouponsScratched>(System.Net.HttpStatusCode.BadRequest, result);
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
                                if (string.IsNullOrEmpty(user.ServiceId) || user.ServiceId == "0")
                                {
                                    CommonResponse cres1 = new CommonResponse();
                                    cres1 = CommonApiMethod.ReturnBadRequestMessage("ServiceId Not Found");
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                                    return response;
                                }
                                if (string.IsNullOrEmpty(user.CouponCode) || user.CouponCode == "0")
                                {
                                    CommonResponse cres1 = new CommonResponse();
                                    cres1 = CommonApiMethod.ReturnBadRequestMessage("CouponCode Not Found");
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                                    return response;
                                }
                                if (res != null && res.Id != 0)
                                {
                                    list = new List<AddCouponsScratched>();
                                    AddCouponsScratched outobject = new AddCouponsScratched();
                                    GetCouponsScratched inobject = new GetCouponsScratched();
                                    inobject.CheckDelete = 0;
                                    inobject.CheckActive = 1;
                                    inobject.GenderStatus = res.Gender;
                                    inobject.KycStatus = res.IsKYCApproved;
                                    inobject.MemberId = Convert.ToInt64(user.MemberId);
                                    inobject.CouponCode = user.CouponCode;
                                    inobject.ServiceId = Convert.ToInt32(user.ServiceId);
                                    //inobject.IsExpired = 0;
                                    //inobject.IsUsed = 0;
                                    inobject.Take = user.Take;
                                    inobject.Skip = Convert.ToInt32(user.Skip) * Convert.ToInt32(user.Take);
                                    inobject.CouponType = (int)AddCoupons.CouponTypeEnum.Coupon;
                                    list = RepCRUD<GetCouponsScratched, AddCouponsScratched>.GetRecordList(MyPay.Models.Common.Common.StoreProcedures.sp_CouponsScratched_Get, inobject, outobject);
                                    if (list.Count > 0)
                                    {
                                        result.status = true;
                                        result.data = list;
                                        result.Message = "Success";
                                        result.ReponseCode = 1;
                                        response.StatusCode = HttpStatusCode.Accepted;
                                        response = Request.CreateResponse<Res_CouponsScratched>(System.Net.HttpStatusCode.OK, result);
                                    }
                                    else
                                    {
                                        cres = CommonApiMethod.ReturnBadRequestMessage("Coupon Not Found");
                                        response.StatusCode = HttpStatusCode.BadRequest;
                                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                        return response;
                                    }
                                }
                                else
                                {
                                    cres = CommonApiMethod.ReturnBadRequestMessage("User Not Found");
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                    return response;
                                }
                            }
                        }
                        log.Info($"{System.DateTime.Now.ToString()} GetScratchedCoupons completed" + Environment.NewLine);
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
                        log.Error($"{System.DateTime.Now.ToString()} GetScratchedCoupons {ex.ToString()} " + Environment.NewLine);
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                    }
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