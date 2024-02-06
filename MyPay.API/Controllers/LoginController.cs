using log4net;
using MyPay.API.Models;
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
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MyPay.API.Controllers
{
    public class LoginController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(LoginController));
        public HttpResponseMessage Post(Req_Login user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside LoginController"+Environment.NewLine);

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
                    string md5hash = Common.getHashMD5(userInput);  //Common.CheckHash(user);
                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results.ToLower() != "success")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }

                    user.Password = Common.Decryption(user.Password);
                    if (user.Password == "error")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage("We implemented new security in our application.Please update the application from playstore or appstore.Sorry for inconvenience!");
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }

                    string authenticationToken = Request.Headers.Authorization.Parameter;
                    Common.authenticationToken = authenticationToken;
                    string msg = "";
                    user.DeviceId = user.DeviceId.Replace("’", "").Replace("?", "");
                    AddUserLoginWithPin res = RepUser.Login(ref msg, authenticationToken, user.PhoneNumber, user.Password, true, user.PlatForm, user.DeviceCode, user.DeviceId, user.PlatForm + ":" + user.Version_Number);
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
                        //inobject.ContactNumber = user.PhoneNumber;                        
                        //AddUser res = RepCRUD<GetUser, AddUser>.GetRecord("sp_Users_Get", inobject, outobject);
                        if (res != null && res.Id != 0)
                        {
                            result.Name = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                            result.EmailId = res.Email;
                            result.ContactNumber = res.ContactNumber;
                            result.PhoneExt = res.PhoneExtension;
                            result.Gender = res.Gender;
                            result.IsKycVerified = res.IsKYCApproved;
                            result.Value = Common.EncryptString(res.MemberId.ToString());
                            result.MemberId = res.MemberId;
                            result.IsDetailUpdated = res.FirstName != "" ? true : false;
                            result.IsPasswordCreated = res.Password != "" ? true : false;
                            result.IsPhoneVerified = res.IsPhoneVerified;
                            result.IsEmailVerified = res.IsEmailVerified;
                            result.IsPinCreated = res.Pin == "" ? false : true;
                            result.TotalAmount = Convert.ToDecimal(res.TotalAmount).ToString("f2");
                            result.RefCode = res.RefCode;
                            result.IsResetPasswordFromAdmin = res.IsResetPasswordFromAdmin;
                            result.Message = msg;
                            result.ReponseCode = 1;
                            result.Token = res.JwtToken;
                            result.status = true;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_UserDetail>(System.Net.HttpStatusCode.OK, result);
                            log.Info($"{System.DateTime.Now.ToString()}  LoginController Completed"+Environment.NewLine);
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
            catch (DbEntityValidationException e)
            {
                string error = "";
                foreach (var eve in e.EntityValidationErrors)
                {                  
                    foreach (var ve in eve.ValidationErrors)
                    {
                        error+= ve.PropertyName+":"+ ve.ErrorMessage;
                       
                    }
                }
                CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(error);
                response.StatusCode = HttpStatusCode.BadRequest;
                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                return response;
             
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} LoginController {ex.ToString()}"+Environment.NewLine);
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