using log4net;
using MyPay.API.Models;
using MyPay.API.Models.Request;
using MyPay.API.Models.Response;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class GetUserBankDetailController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(GetUserBankDetailController));

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/getuserbankdetail")]
        public HttpResponseMessage GetUserBankDetail(Req_GetUserBankDetail user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside GetUserBankDetail" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            List<AddUserBankDetail> list = new List<AddUserBankDetail>();
            List<AddUserBankDetail> newlist = new List<AddUserBankDetail>();
            Res_GetUserBankDetail result = new Res_GetUserBankDetail();
            var response = Request.CreateResponse<Res_GetUserBankDetail>(System.Net.HttpStatusCode.BadRequest, result);

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
                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
                        if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString().Trim() != "0")
                        {
                            int VendorAPIType = 0;
                            int Type = 0;
                            Int64 memId = Convert.ToInt64(user.MemberId);

                            AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                            resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "", false, user.Mpin, "", false, true);
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
                        AddUserBankDetail outobject = new AddUserBankDetail();
                        GetUserBankDetail inobject = new GetUserBankDetail();
                        inobject.CheckActive = 1;
                        inobject.CheckDelete = 0;
                        inobject.MemberId = user.MemberId;
                        if (user.IsPrimary == true)
                        {
                            inobject.CheckPrimary = 1;
                        }
                        list = RepCRUD<GetUserBankDetail, AddUserBankDetail>.GetRecordList(Common.StoreProcedures.sp_UserBankDetail_Get, inobject, outobject);
                        foreach (var item in list)
                        {
                            AddUserBankDetail bank = new AddUserBankDetail();
                            //bank.ICON_NAME = Common.LiveSiteUrl + "/Content/assets/Images/BanksLogos/M-Banking/" + item.ICON_NAME;
                            bank.ICON_NAME = item.ICON_NAME;
                            bank.Id = item.Id;
                            bank.IsActive = item.IsActive;
                            bank.IsApprovedByAdmin = item.IsApprovedByAdmin;
                            bank.IsDeleted = item.IsDeleted;
                            bank.IsPrimary = item.IsPrimary;
                            bank.MemberId = item.MemberId;
                            bank.Name = item.Name;
                            bank.Sno = item.Sno;
                            bank.UpdatedBy = item.UpdatedBy;
                            bank.UpdatedDate = item.UpdatedDate;
                            bank.UpdatedDateDt = item.UpdatedDateDt;
                            bank.BranchId = item.BranchId;
                            bank.BranchName = item.BranchName;
                            bank.BankCode = item.BankCode;
                            bank.BankName = item.BankName;
                            bank.AccountNumber = item.AccountNumber;
                            bank.CreatedBy = item.CreatedBy;
                            bank.CreatedByName = item.CreatedByName;
                            bank.CreatedDate = item.CreatedDate;
                            bank.CreatedDateDt = item.CreatedDateDt;
                            bank.BankTransferType = item.BankTransferType;
                            newlist.Add(bank);
                        }
                        int BankTransferType = (int)AddUserBankDetail.UserBankType.NPS;
                        using (MyPayEntities db = new MyPayEntities())
                        {
                            ApiSetting objApiSettings = db.ApiSettings.FirstOrDefault();
                            if (objApiSettings != null && objApiSettings.BankTransferType > 0)
                            {
                                if (objApiSettings.BankTransferType != 2)
                                {
                                    BankTransferType = (int)AddUserBankDetail.UserBankType.NCHL;
                                }
                            }
                        }
                        result.BankTransferType = BankTransferType;
                        result.IsEmailVerified = resGetRecord.IsEmailVerified;
                        result.status = true;
                        result.data = newlist;
                        result.ReponseCode = 1;
                        result.Message = "Success";
                        response.StatusCode = HttpStatusCode.Created;
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} GetUserBankDetail completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} GetUserBankDetail {ex.ToString()} " + Environment.NewLine);
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