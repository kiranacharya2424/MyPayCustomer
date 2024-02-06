using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using log4net;
using MyPay.API.Models;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Repository;

namespace MyPay.API.Controllers
{
    public class LoginWithPinController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(LoginWithPinController));
        public HttpResponseMessage Post(Req_LoginWithPin user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside LoginWithPinController" + Environment.NewLine);

            string userInput = getRawPostData().Result;



            Res_UserDetail result = new Res_UserDetail();
            var response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, result);
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

                    string md5hash = Common.getHashMD5(userInput); //Common.CheckHash(user, "No");

                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results.ToLower() != "success")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    user.Pin = Common.Decryption(user.Pin);
                    if (user.Pin == "error")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage("We implemented new security in our application.Please update the application from playstore or appstore.Sorry for inconvenience!");
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    string authenticationToken = Request.Headers.Authorization.Parameter;
                    Common.authenticationToken = authenticationToken;
                    string msg = "";
                    AddUserLoginWithPin res = RepUser.LoginWithPin(ref msg, authenticationToken, user.MemberId, user.Pin, true, user.PlatForm, user.DeviceCode, user.DeviceId, user.PlatForm + ":" + user.Version_Number);
                    if (msg.ToLower() != "success")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    else
                    {
                        //AddUser outobject = new AddUser();                     
                        //GetUser inobject = new GetUser();
                        //inobject.MemberId = user.MemberId;
                        //AddUser res = RepCRUD<GetUser, AddUser>.GetRecord("sp_Users_Get", inobject, outobject);
                        if (res != null && res.Id != 0)
                        {
                            result.Name = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                            result.EmailId = res.Email;
                            result.ContactNumber = res.ContactNumber;
                            result.PhoneExt = res.PhoneExtension;
                            result.Gender = res.Gender;
                            result.IsKycVerified = res.IsKYCApproved;
                            result.MemberId = res.MemberId;
                            result.Value = Common.EncryptString(res.MemberId.ToString());
                            result.IsDetailUpdated = res.FirstName != "" ? true : false;
                            result.IsPasswordCreated = res.Password != "" ? true : false;
                            result.IsPhoneVerified = res.IsPhoneVerified;
                            result.IsEmailVerified = res.IsEmailVerified;
                            result.IsPinCreated = res.Pin == "" ? false : true;
                            result.RefCode = res.RefCode;
                            result.IsResetPasswordFromAdmin = res.IsResetPasswordFromAdmin;
                            result.TotalAmount = Convert.ToDecimal(res.TotalAmount).ToString("f2");
                            result.Message = msg;
                            result.ReponseCode = 1;
                            result.status = true;
                            result.Token = res.JwtToken;
                            result.Value = Common.EncryptString(res.MemberId.ToString());
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_UserDetail>(System.Net.HttpStatusCode.OK, result);
                            log.Info($"{System.DateTime.Now.ToString()} LoginWithPinController completed" + Environment.NewLine);
                            return response;
                        }
                        else
                        {
                            CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage("User Not Found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} LoginWithPinController {ex.ToString()} " + Environment.NewLine);
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