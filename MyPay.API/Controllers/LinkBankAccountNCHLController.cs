using DeviceId;
using log4net;
using MyPay.API.Models;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Response;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using System;
using System.Data.Entity.Validation;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class LinkBankAccountNCHLController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(LinkBankAccountController));
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/LinkBankAccountNCHLToken")]
        public HttpResponseMessage LinkBankAccountNCHLToken(AddRegisterLinkedBankNCHL user)
        {
            CommonResponse cres = new CommonResponse();
            Res_CIPSRegisterLinkedBankNCHL result = new Res_CIPSRegisterLinkedBankNCHL();

            var response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
            try
            {
                string UserInput = getRawPostData().Result;
                if (string.IsNullOrEmpty(user.participantId))
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage("Please enter participantId");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.identifier))
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage("Please enter identifier");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.userIdentifier))
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage("Please enter userIdentifier");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.mobileNo))
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage("Please enter mobileNo");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.email))
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage("Please enter email");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.amount))
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage("Please enter amount");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.debitType))
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage("Please enter debitType");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.frequency))
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage("Please enter frequency");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.mandateStartDate))
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage("Please enter mandateStartDate");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.mandateExpiryDate))
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage("Please enter mandateExpiryDate");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.mandateToken))
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage("Please enter mandateToken");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.mandateTokenType))
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage("Please enter mandateTokenType");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.entryId))
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage("Please enter entryId");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.mandateTokenNickName))
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage("Please enter mandateTokenNickName");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.bankName))
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage("Please enter bankName");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.bankId))
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage("Please enter bankId");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.token))
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage("Please enter token");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }

                string DeviceCode = new DeviceIdBuilder().AddMachineName().AddProcessorId().AddMotherboardSerialNumber().AddSystemDriveSerialNumber().ToString();
                string PlatForm = "Web";
                string ApiResponse = "";

                string Reference = Common.GenerateReferenceUniqueID();
                int VendorApiType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.Internet_Banking;
                string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorApiType).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                AddVendor_API_Requests objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse("", Reference, Common.CreatedBy, Common.CreatedByName, "", "", UserInput, DeviceCode, PlatForm, VendorApiType);
                objVendor_API_Requests.Req_Token = user.token;
                objVendor_API_Requests.Res_Khalti_Output = user.mandateToken;

                string TokenString = user.participantId + "," + user.identifier + "," + user.userIdentifier + "," + user.mobileNo + "," + user.email + "," + user.amount + "," + user.debitType + "," + user.frequency + "," + user.mandateStartDate + "," + user.mandateExpiryDate + "," + user.mandateToken + "," + user.mandateTokenType;
                bool ValidateSignature = Common.VerifyConnectIPSToken_LinkBank(TokenString, user.token);

                Common.AddLogs($"VerifyConnectIPSToken_LinkBank:{ValidateSignature}", false, (int)AddLog.LogType.DBLogs);

                if (ValidateSignature)
                {
                    AddUserLoginWithPin outobjectUser = new AddUserLoginWithPin();
                    GetUserLoginWithPin inobjectUser = new GetUserLoginWithPin();
                    inobjectUser.MemberId = Convert.ToInt64(user.userIdentifier);
                    inobjectUser.CheckDelete = 0;
                    AddUserLoginWithPin resUser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobjectUser, outobjectUser);
                    if (resUser.Id != 0)
                    {
                        AddUserBankDetail outobjectBankDtl = new AddUserBankDetail();
                        GetUserBankDetail inobjectBankDtl = new GetUserBankDetail();
                        inobjectBankDtl.MemberId = Convert.ToInt64(user.userIdentifier);
                        inobjectBankDtl.CheckDelete = 0;
                        inobjectBankDtl.CheckActive = 1;
                        inobjectBankDtl.BankCode = user.bankId;
                        AddUserBankDetail resBankDtl = RepCRUD<GetUserBankDetail, AddUserBankDetail>.GetRecord(Common.StoreProcedures.sp_UserBankDetail_Get, inobjectBankDtl, outobjectBankDtl);
                        if (resBankDtl != null && resBankDtl.Id != 0)
                        {
                            Res_CIPSRegisterData objData = new Res_CIPSRegisterData();
                            string tokenString = user.identifier + "," + user.participantId + "," + user.entryId;
                            objData.token = Common.GenerateConnectIPSToken_LinkBank(tokenString);
                            objData.identifier = user.identifier;
                            objData.entryId = user.entryId;
                            objData.participantId = user.participantId;

                            result.data = objData;
                            result.responseCode = "111";
                            result.responseMessage = "FAILED";
                            response = Request.CreateResponse<Res_CIPSRegisterLinkedBankNCHL>(System.Net.HttpStatusCode.Created, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);

                        }
                        else
                        {
                            string BranchId = string.Empty;

                            AddBankList outobjectbank = new AddBankList();
                            GetBankList inobjectbank = new GetBankList();
                            inobjectbank.BANK_CD = user.bankId;
                            AddBankList resbank = RepCRUD<GetBankList, AddBankList>.GetRecord(Common.StoreProcedures.sp_BankList_Get, inobjectbank, outobjectbank);
                            if (resbank != null && resbank.Id != 0)
                            {
                                resBankDtl.BranchId = resbank.BRANCH_CD;
                                resBankDtl.BranchName = resbank.BRANCH_NAME;
                                resBankDtl.ICON_NAME = Common.LiveSiteUrl + "/Content/assets/Images/BanksLogos/M-Banking/" + resbank.ICON_NAME;
                            }
                            resBankDtl.MemberId = Convert.ToInt64(user.userIdentifier);
                            resBankDtl.BankCode = user.bankId;
                            resBankDtl.BankName = user.bankName;
                            resBankDtl.BranchName = user.mandateToken;
                            resBankDtl.AccountNumber = user.mandateTokenNickName;
                            resBankDtl.TransactionId = user.identifier;
                            resBankDtl.Name = resUser.FirstName + " " + resUser.LastName;
                            resBankDtl.IsPrimary = true;
                            resBankDtl.BankTransferType = (int)AddUserBankDetail.UserBankType.NCHL;
                            resBankDtl.IsActive = true;
                            resBankDtl.IsDeleted = false;
                            resBankDtl.TransactionId = user.identifier;
                            resBankDtl.CreatedBy = Common.CreatedBy;
                            resBankDtl.CreatedByName = Common.CreatedByName;
                            AddUserBankDetail outobject_primary = new AddUserBankDetail();
                            GetUserBankDetail inobject_primary = new GetUserBankDetail();
                            inobject_primary.MemberId = Convert.ToInt64(user.userIdentifier);
                            inobject_primary.CheckPrimary = 1;
                            AddUserBankDetail res_primary = RepCRUD<GetUserBankDetail, AddUserBankDetail>.GetRecord(Common.StoreProcedures.sp_UserBankDetail_Get, inobject_primary, outobject_primary);
                            if (res_primary != null && res_primary.Id != 0)
                            {
                                res_primary.IsPrimary = false;
                                bool status_primary = RepCRUD<AddUserBankDetail, GetUserBankDetail>.Update(res_primary, "userbankdetail");
                            }

                            Int64 Id = RepCRUD<AddUserBankDetail, GetUserBankDetail>.Insert(resBankDtl, "userbankdetail");

                            if (Id > 0)
                            {
                                resUser.IsBankAdded = 1;
                                bool IsBankAdded = resUser.UpdateIsBankAdded(resUser.Id, resUser.IsBankAdded);
                                string mystring = File.ReadAllText(HttpContext.Current.Server.MapPath("/Templates/BankAccountLinked.html"));
                                string body = mystring;
                                if (!string.IsNullOrEmpty(resUser.FirstName))
                                {
                                    body = body.Replace("##UserName##", resUser.FirstName);
                                }
                                else
                                {
                                    body = body.Replace("##UserName##", String.Empty);
                                }
                                body = body.Replace("##AccountHolderName##", resBankDtl.Name);
                                body = body.Replace("##AccountNumber##", resBankDtl.AccountNumber);
                                body = body.Replace("##BankName##", resBankDtl.BankName);

                                string Subject = MyPay.Models.Common.Common.WebsiteName + " - Bank Account Linked";
                                if (!string.IsNullOrEmpty(resUser.Email))
                                {
                                    Common.SendAsyncMail(resUser.Email, Subject, body);
                                }
                                Common.AddLogs("User bank detail added successfully", false, Convert.ToInt32(AddLog.LogType.User), resUser.MemberId, resUser.FirstName, false, "", "");

                                result.responseCode = "000";
                                result.responseMessage = "Success";
                            }
                            else
                            {
                                result.responseCode = "111";
                                result.responseMessage = "FAILED";
                            }
                            Res_CIPSRegisterData objData = new Res_CIPSRegisterData();
                            string tokenString = user.identifier + "," + user.participantId + "," + user.entryId;
                            objData.token = Common.GenerateConnectIPSToken_LinkBank(tokenString);
                            objData.identifier = user.identifier;
                            objData.entryId = user.entryId;
                            objData.participantId = user.participantId;

                            result.data = objData;
                            response = Request.CreateResponse<Res_CIPSRegisterLinkedBankNCHL>(System.Net.HttpStatusCode.Created, result);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        }
                    }
                    else
                    {
                        cres = CommonApiMethod.ReturnBadRequestMessage("Invalid User Details");
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                    }
                }
                else
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage("Invalid Data");
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                }
                objVendor_API_Requests.Res_Output = ApiResponse;
                RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "vendor_api_requests");

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
                log.Error($"{System.DateTime.Now.ToString()} LinkBankAccount {ex.ToString()} " + Environment.NewLine);
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