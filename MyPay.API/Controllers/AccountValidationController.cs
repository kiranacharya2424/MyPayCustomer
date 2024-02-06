using log4net;
using MyPay.API.Models;
using MyPay.API.Models.Response;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using Newtonsoft.Json;
using System;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class AccountValidationController : ApiController
    {

        //[System.Web.Http.HttpPost]
        //public HttpResponseMessage AccountValidation(Req_AccountValidation user)
        //{
        //    CommonResponse cres = new CommonResponse();
        //    var response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
        //    try
        //    {
        //        if (Request.Headers.Authorization == null)
        //        {
        //            string results = "Un-Authorized Request";
        //            cres = CommonApiMethod.ReturnBadRequestMessage(results);
        //            response.StatusCode = HttpStatusCode.BadRequest;
        //            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
        //            return response;
        //        }
        //        else
        //        {
        //            string md5hash = Common.CheckHash(user);
        //            string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode,user.SecretKey);
        //            if (results != "Success")
        //            {
        //                cres = CommonApiMethod.ReturnBadRequestMessage(results);
        //                response.StatusCode = HttpStatusCode.BadRequest;
        //                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
        //                return response;
        //            }
        //            else
        //            {
        //                if (string.IsNullOrEmpty(user.bankId))
        //                {
        //                    cres = CommonApiMethod.ReturnBadRequestMessage("Please Enter Bank Id");
        //                    response.StatusCode = HttpStatusCode.BadRequest;
        //                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
        //                    return response;
        //                }
        //                else if (string.IsNullOrEmpty(user.accountId))
        //                {
        //                    cres = CommonApiMethod.ReturnBadRequestMessage("Please Enter Account Number");
        //                    response.StatusCode = HttpStatusCode.BadRequest;
        //                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
        //                    return response;
        //                }
        //                else if (string.IsNullOrEmpty(user.accountName))
        //                {
        //                    cres = CommonApiMethod.ReturnBadRequestMessage("Please Enter Account Name");
        //                    response.StatusCode = HttpStatusCode.BadRequest;
        //                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
        //                    return response;
        //                }

        //                GetFundAccountValidation result = RepNps.FundAccountValidation(user.accountName, user.accountId, user.bankId);
        //                response = Request.CreateResponse<GetFundAccountValidation>(System.Net.HttpStatusCode.Created, result);
        //                return response;

        //            }
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
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //
        //}

        private static ILog log = LogManager.GetLogger(typeof(AccountValidationController));
        [System.Web.Http.HttpPost]
        public HttpResponseMessage AccountValidation(Req_AccountValidation user)
        {
            log.Info($" {System.DateTime.Now.ToString()} inside AccountValidation Started" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            var response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
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
                        int VendorAPIType = 0;
                        int Type = 0;
                        Int64 memId = 0;
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, UserInput, "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "", false, user.Mpin, "", true, true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }

                        if (string.IsNullOrEmpty(user.bankId))
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Please Enter Bank Id");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        else if (string.IsNullOrEmpty(user.accountId))
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Please Enter Account Number");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        else if (string.IsNullOrEmpty(user.accountName))
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Please Enter Account Name");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        string ApiResponse = string.Empty;
                        AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                        string authenticationToken = Request.Headers.Authorization.Parameter;
                        Common.authenticationToken = authenticationToken;
                        bool IsCouponUnlocked = false; string TransactionID = string.Empty;
                        string Reference = Common.GenerateReferenceUniqueID();
                        int VendorApiType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.bank_transfer;
                        string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorApiType).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                        objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse("", Reference, resGetRecord.MemberId, resGetRecord.FirstName + " " + resGetRecord.LastName, "", authenticationToken, UserInput, user.DeviceCode, user.PlatForm, VendorApiType);


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
                            string token = RepNCHL.gettoken();
                            string ApiName = "api/validatebankaccount";
                            string data = RepNCHL.PostMethodWithToken(ApiName, JsonConvert.SerializeObject(user), token);
                            objVendor_API_Requests.Req_Khalti_URL = ApiName;
                            Common.AddLogs($"URL: {ApiName} Request: {JsonConvert.SerializeObject(user)} Response:{data} ", false, (int)AddLog.LogType.DBLogs);
                            if (!string.IsNullOrEmpty(data))
                            {
                                Res_AccountValidation res = JsonConvert.DeserializeObject<Res_AccountValidation>(data);
                                if ((res.responseCode == "000" || res.responseCode == "999"))
                                {
                                    //List<GetNCBranchList> list = RepNCHL.GetBranchList("cips", "");
                                    //GetNCBranchList objList = list.Where(c => c.branchId == res.branchId).FirstOrDefault();
                                    //res.branchName = objList.branchName;
                                    res.ReponseCode = 1;
                                    res.Message = "Success";
                                    res.Details = res.responseMessage;
                                    res.status = true;
                                    response.StatusCode = HttpStatusCode.Created;
                                    response = Request.CreateResponse<Res_AccountValidation>(System.Net.HttpStatusCode.Created, res);
                                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(res);

                                    objVendor_API_Requests.Res_Output = ApiResponse;
                                    RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "vendor_api_requests");
                                    return response;
                                }
                                else
                                {
                                    res.Details = res.responseMessage;
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    response = Request.CreateResponse<Res_AccountValidation>(System.Net.HttpStatusCode.BadRequest, res);
                                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(res);

                                    objVendor_API_Requests.Res_Output = ApiResponse;
                                    RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "vendor_api_requests");
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
                                RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "vendor_api_requests");
                                return response;
                            }
                        }
                        else
                        {
                            objVendor_API_Requests.Req_Khalti_URL = "v1/AccountValidation";
                            GetFundAccountValidation valid = RepNps.FundAccountValidation(user.accountName, user.accountId, user.bankId);
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
                                RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "vendor_api_requests");
                                return response;
                            }
                            else
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage(valid.message);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                                objVendor_API_Requests.Res_Output = ApiResponse;
                                RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "vendor_api_requests");
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