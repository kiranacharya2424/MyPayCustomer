using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using log4net;
using Microsoft.IdentityModel.Tokens;
using MyPay.API.Models;
using MyPay.API.Models.Request;
using MyPay.API.Models.Response;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Repository;
using Newtonsoft.Json;

namespace MyPay.API.Controllers
{

    public class GetUserWebTokenController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(GetUserWalletWithKYCController));

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/GetUserWebToken")]
        public HttpResponseMessage Post(Req_GetUserDetail user)
        {
            log.Info($" {System.DateTime.Now.ToString()} inside GetUserWalletWithKYCController  {Environment.NewLine}");
            string WebPrefix = Common.LiveSiteUrl;
            Res_UserWalletWithKYC result = new Res_UserWalletWithKYC();
            var response = Request.CreateResponse<Res_UserWalletWithKYC>(System.Net.HttpStatusCode.BadRequest, result);
            try
            {
                if (Request.Headers.Authorization == null)
                {
                    string results = "Un-Authorized Request";
                    CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(results);
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    return response;
                }
                else
                {
                    string md5hash = Common.CheckHash(user);

                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results.ToLower() != "success")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }

                    if (string.IsNullOrEmpty(user.DeviceId))
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage("Please enter DeviceId");
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    else
                    {
                        AddUserLoginWithPin res = new AddUserLoginWithPin();
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        if (!string.IsNullOrEmpty(user.MemberId) && user.MemberId != "0")
                        {
                            Int64 memId = Convert.ToInt64(user.MemberId);

                            string retMsg = string.Empty;
                            string authenticationToken = Request.Headers.Authorization.Parameter;
                            if (Common.GetCreatedByName(authenticationToken).ToLower() == "web")
                            {
                                AddUserLoginWithPin outobjectUser = new AddUserLoginWithPin();
                                GetUserLoginWithPin inobjectUser = new GetUserLoginWithPin();
                                inobjectUser.DeviceId = user.DeviceId;
                                inobjectUser.MemberId = Convert.ToInt64(user.MemberId);
                                AddUserLoginWithPin resUser = RepCRUD<GetUserLoginWithPin, AddUserLoginWithPin>.GetRecord(Common.StoreProcedures.sp_Users_GetLoginWithPin, inobjectUser, outobjectUser);
                                if (resUser.Id > 0)
                                {
                                    string originalFileName = Common.EncryptString(resUser.ContactNumber).Replace("/", "@@@");
                                    string TokenUpdatedTime = string.Empty;
                                    string filepath = HttpContext.Current.Server.MapPath("/UserCodes/" + originalFileName + ".txt");
                                    if (!System.IO.File.Exists(filepath))
                                    {
                                        retMsg = Common.Invalidusertoken;
                                    }
                                    if (resUser.IsActive == false)
                                    {
                                        retMsg = Common.InactiveUserMessage;
                                    }
                                    TokenUpdatedTime = (File.ReadAllText(filepath));
                                    if (retMsg == "")
                                    {
                                        string NewTokenUpdatedTime = DateTime.UtcNow.AddMinutes(15).ToString("dd-MMM-yyyy HH:mm:ss");
                                        System.IO.File.WriteAllText(filepath, NewTokenUpdatedTime);
                                        retMsg = "Success";
                                    }
                                }
                                else
                                {
                                    retMsg = Common.SessionExpired;
                                }
                            }
                            else
                            {
                                retMsg = Common.SessionExpired;
                            }
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(retMsg);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        else
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage("MemberId Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"  {System.DateTime.Now.ToString()}  GetUserWalletWithKYCController {ex.ToString()}  ");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }


        }
         
    }
}