using log4net;
using Microsoft.IdentityModel.Tokens;
using MyPay.API.Models;
using MyPay.Models;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class UserAuthorizationController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(UserAuthorizationController));
        private DateTime _ExpirationTime = DateTime.UtcNow.AddMinutes(60);
        [HttpGet]
        public string GetToken(string username, string pwd, long memberid)
        {
            string jwt_token = Common.GetToken(username, pwd, memberid, ref _ExpirationTime);
            return jwt_token;
        }

        // [Authorize]
        public HttpResponseMessage Post(Req_UserAuthentication user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside UserAuthorizationController" + Environment.NewLine);
            Res_UserAuthorization result = new Res_UserAuthorization();
            var response = Request.CreateResponse<Res_UserAuthorization>(System.Net.HttpStatusCode.BadRequest, result);
            try
            {
                if (string.IsNullOrEmpty(user.UserName))
                {
                    result.Message = "Please enter username.";
                    result.ReponseCode = 2;
                    response = Request.CreateResponse<Res_UserAuthorization>(System.Net.HttpStatusCode.OK, result);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.Password))
                {
                    result.Message = "Please enter Password.";
                    result.ReponseCode = 2;
                    response = Request.CreateResponse<Res_UserAuthorization>(System.Net.HttpStatusCode.OK, result);
                    return response;
                }
                else
                {
                    AddUserAuthorization outobject = new AddUserAuthorization();
                    GetUserAuthorization inobject = new GetUserAuthorization();
                    inobject.UserName = user.UserName;
                   // inobject.PassWord = user.Password;
                    AddUserAuthorization res = RepCRUD<GetUserAuthorization, AddUserAuthorization>.GetRecord("sp_UserAuthorization_Get", inobject, outobject);
                    string password=res.Password;
                    if (Common.DecryptString(res.Password) != user.Password)
                    {
                        result.Message = "Incorrect Password";
                        result.ReponseCode = 4;
                        response = Request.CreateResponse<Res_UserAuthorization>(System.Net.HttpStatusCode.OK, result);
                        return response;
                    }
                    if (res.IPAddress != Common.GetUserIP() && (res.IPAddress != Common.GetServerIPAddress()))
                    {
                        result.Message = "Invalid IP Address";
                        result.ReponseCode = 4;
                        response = Request.CreateResponse<Res_UserAuthorization>(System.Net.HttpStatusCode.OK, result);
                        return response;
                    }
                    string token = GetToken(user.UserName, user.Password, res.MemberId);
                    if (string.IsNullOrEmpty(token))
                    {
                        result.Message = "Token not found.";
                        result.ReponseCode = 4;
                        response = Request.CreateResponse<Res_UserAuthorization>(System.Net.HttpStatusCode.OK, result);
                        return response;
                    }
                    else
                    {
                        res.Token = token;
                        res.ExpiryDate = _ExpirationTime;
                        bool status = RepCRUD<AddUserAuthorization, GetUserAuthorization>.Update(res, "userauthorization");
                        if (status)
                        {
                            result.Message = "Token generated.";
                            result.status = true;
                            result.Token = res.Token;
                            result.ReponseCode = 1;
                            result.ExpirationTime = res.ExpiryDate.ToString("d-MMM-yyy hh:mm:ss tt");
                            response = Request.CreateResponse<Res_UserAuthorization>(System.Net.HttpStatusCode.OK, result);
                            log.Info($"{System.DateTime.Now.ToString()} UserAuthorizationController completed" + Environment.NewLine);
                            return response;
                        }
                        else
                        {
                            result.Message = "Token not updated.";
                            result.ReponseCode = 3;
                            response = Request.CreateResponse<Res_UserAuthorization>(System.Net.HttpStatusCode.OK, result);
                            return response;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} UserAuthorizationController {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [Route("api/getapiuser")]
        [HttpPost]
        public HttpResponseMessage GetAPIUser(Req_UserAuthentication user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside getapiuser" + Environment.NewLine);
            Res_UserAuthorization_GetToken result = new Res_UserAuthorization_GetToken();
            var response = Request.CreateResponse<Res_UserAuthorization_GetToken>(System.Net.HttpStatusCode.BadRequest, result);
            try
            {
                if (string.IsNullOrEmpty(user.UserName))
                {
                    result.Message = "Please enter username.";
                    result.ReponseCode = 2;
                    response = Request.CreateResponse<Res_UserAuthorization_GetToken>(System.Net.HttpStatusCode.OK, result);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.Password))
                {
                    result.Message = "Please enter Password.";
                    result.ReponseCode = 2;
                    response = Request.CreateResponse<Res_UserAuthorization_GetToken>(System.Net.HttpStatusCode.OK, result);
                    return response;
                }
                else
                {
                    AddUserAuthorization outobject = new AddUserAuthorization();
                    GetUserAuthorization inobject = new GetUserAuthorization();
                    inobject.UserName = user.UserName;
                    AddUserAuthorization res = RepCRUD<GetUserAuthorization, AddUserAuthorization>.GetRecord("sp_UserAuthorization_Get", inobject, outobject);
                    if (res != null && res.Id != 0)
                    {
                        if (Common.DecryptString(res.Password) != user.Password)
                        {
                            result.Message = "Incorrect Password";
                            result.ReponseCode = 4;
                            response = Request.CreateResponse<Res_UserAuthorization_GetToken>(System.Net.HttpStatusCode.OK, result);
                            return response;
                        }
                        else
                        {
                            result.Message = "success";
                            result.status = true;
                            result.Token = res.Token;
                            result.ReponseCode = 1;
                            response = Request.CreateResponse<Res_UserAuthorization_GetToken>(System.Net.HttpStatusCode.OK, result);
                            log.Info($"{System.DateTime.Now.ToString()} getapiuser completed" + Environment.NewLine);
                            return response;
                        }
                    }
                    else
                    {
                        result.Message = "Token not found.";
                        result.ReponseCode = 3;
                        response = Request.CreateResponse<Res_UserAuthorization_GetToken>(System.Net.HttpStatusCode.OK, result);
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} getapiuser {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("api/UpdateAuthorizationIp")]
        public HttpResponseMessage UserAuthorizationUpdateIp(Req_UserAuthentication user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside UpdateAuthorizationIp" + Environment.NewLine);
            Res_UserAuthorization result = new Res_UserAuthorization();
            var response = Request.CreateResponse<Res_UserAuthorization>(System.Net.HttpStatusCode.BadRequest, result);
            try
            {
                if (string.IsNullOrEmpty(user.UserName))
                {
                    result.Message = "Please enter username.";
                    result.ReponseCode = 2;
                    response = Request.CreateResponse<Res_UserAuthorization>(System.Net.HttpStatusCode.OK, result);
                    return response;
                }
                else if (string.IsNullOrEmpty(user.Password))
                {
                    result.Message = "Please enter Password.";
                    result.ReponseCode = 2;
                    response = Request.CreateResponse<Res_UserAuthorization>(System.Net.HttpStatusCode.OK, result);
                    return response;
                }
                else
                {
                    AddUserAuthorization outobject = new AddUserAuthorization();
                    GetUserAuthorization inobject = new GetUserAuthorization();
                    inobject.UserName = user.UserName;
                    AddUserAuthorization res = RepCRUD<GetUserAuthorization, AddUserAuthorization>.GetRecord("sp_UserAuthorization_Get", inobject, outobject);
                    if (Common.DecryptString(res.Password) != user.Password)
                    {
                        result.Message = "Incorrect Password";
                        result.ReponseCode = 4;
                        response = Request.CreateResponse<Res_UserAuthorization>(System.Net.HttpStatusCode.OK, result);
                        return response;
                    }
                    res.IPAddress = Common.GetUserIP();
                    bool status = RepCRUD<AddUserAuthorization, GetUserAuthorization>.Update(res, "userauthorization");
                    if (status)
                    {
                        result.Message = "Ip Updated successfully.";
                        result.status = true;
                        result.ReponseCode = 1;
                        result.ExpirationTime = res.ExpiryDate.ToString("d-MMM-yyy hh:mm:ss tt");
                        response = Request.CreateResponse<Res_UserAuthorization>(System.Net.HttpStatusCode.OK, result);
                        log.Info($"{System.DateTime.Now.ToString()} UpdateAuthorizationIp completed" + Environment.NewLine);
                        return response;
                    }
                    else
                    {
                        result.Message = "Ip Address not updated.";
                        result.ReponseCode = 3;
                        response = Request.CreateResponse<Res_UserAuthorization>(System.Net.HttpStatusCode.OK, result);
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} UpdateAuthorizationIp {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
