using log4net;
using MyPay.API.Models;
using MyPay.API.Models.State;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class GetTransferBankListController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(GetTransferBankListController));

        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetTransferBankList(Req_State user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside GetTransferBankList" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            List<BankList> list = new List<BankList>();
            Res_BankList result = new Res_BankList();
            var response = Request.CreateResponse<Res_BankList>(System.Net.HttpStatusCode.BadRequest, result);
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
                        AddUserLoginWithPin res = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        Int64 memId = 0;
                        int VendorAPIType = 0;
                        int Type = 0;
                        res = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
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
                        if (BankType != "NPS")
                        {
                            List<BankList> listBankList = RepNCHL.GetDataBankList();
                            List<GetNCBankList> listnchl = RepNCHL.GetBankList("cips");
                            if (listnchl.Count > 0)
                            {
                                foreach (var item in listnchl)
                                {
                                    BankList ba = new BankList();
                                    ba = listBankList.Where(c => c.BANK_CD == item.bankId).FirstOrDefault();
                                    //ba.BANK_CD = item.BANK_CD;
                                    //ba.BANK_NAME = item.BANK_NAME;
                                    //ba.BRANCH_NAME = item.BRANCH_NAME;
                                    //ba.BRANCH_CD = item.BRANCH_CD;
                                    //ba.ICON_NAME = item.ICON_NAME;
                                    if (ba != null && ba.Id > 0)
                                    {
                                        ba.BankTransferType = (int)AddUserBankDetail.UserBankType.NCHL;
                                        list.Add(ba);
                                    }
                                }
                                list.Sort((x, y) => Convert.ToString(x.BANK_NAME).CompareTo(Convert.ToString(y.BANK_NAME)));

                                result.status = true;
                                result.data = list;
                                result.ReponseCode = 1;
                                response.StatusCode = HttpStatusCode.Created;
                                result.Message = "Success";
                            }
                            else
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage("Data Not Found");
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                return response;
                            }
                        }
                        else
                        {
                            GetFundBankList listnps = RepNps.GetFundBankList();
                            if (listnps.data.Count > 0)
                            {
                                foreach (var item in listnps.data)
                                {
                                    BankList ba = new BankList();
                                    ba.BANK_CD = item.InstrumentCode;
                                    ba.BANK_NAME = item.InstitutionName;
                                    ba.BRANCH_NAME = item.InstitutionName;
                                    ba.BRANCH_CD = "1";
                                    ba.ICON_NAME = item.LogoUrl;
                                    ba.BankTransferType = (int)AddUserBankDetail.UserBankType.NPS;
                                    list.Add(ba);
                                }
                                list = list.OrderBy(c => c.BRANCH_NAME).ToList();
                                result.status = true;
                                result.data = list;
                                result.ReponseCode = 1;
                                response.StatusCode = HttpStatusCode.Created;
                                result.Message = "Success";
                            }
                            else
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage("Data Not Found");
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                return response;
                            }
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} GetTransferBankList completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} GetTransferBankList {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}