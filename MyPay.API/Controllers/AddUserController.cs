using MyPay.API.Models;
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
using MyPay.Models.Common;
using MyPay.Models.Add;
using System.Threading.Tasks;
using System.IO;
using log4net;

namespace MyPay.API.Controllers
{
    public class AddUserController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(AddUserController));
        public HttpResponseMessage Post(Req_User user)
        {
            log.Info($"  {System.DateTime.Now.ToString()} inside AddUserController   { Environment.NewLine}");
            string UserInput = getRawPostData().Result;


            Res_UserDetail result = new Res_UserDetail();
            var response = Request.CreateResponse<Res_UserDetail>(System.Net.HttpStatusCode.BadRequest, result);
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
                    string md5hash = Common.getHashMD5(UserInput); //Common.CheckHash(user);

                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results.ToLower() != "success")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                   
                    string authenticationToken = Request.Headers.Authorization.Parameter;
                    Common.authenticationToken = authenticationToken;
                    Int64 MemberID_Return = 0;
                    AddUser res = new AddUser();
                    bool IsCouponUnlocked = false;
                    string msg = RepUser.Register(ref IsCouponUnlocked, ref res, user.DeviceId, user.Lat, user.Lon, user.ContactNumber, user.RefCode, user.PhoneExt, user.VerificationOtp, user.PlatForm, user.DeviceCode, true, UserInput, authenticationToken, ref MemberID_Return);
                    if (msg.ToLower() != "success" && !msg.ToLower().Contains("already has an account"))
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
                        //inobject.ContactNumber = user.ContactNumber;
                        //res = RepCRUD<GetUser, AddUser>.GetRecord("sp_Users_Get", inobject, outobject);
                        if (res != null && res.MemberId != 0)
                        {

                            result.Name = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                            result.EmailId = res.Email;
                            result.Gender = res.Gender;
                            result.IsKycVerified = res.IsKYCApproved;
                            result.MemberId = res.MemberId;
                            result.IsDetailUpdated = res.FirstName != "" ? true : false;
                            result.IsPasswordCreated = res.Password != "" ? true : false;
                            result.IsPhoneVerified = res.IsPhoneVerified;
                            result.IsEmailVerified = res.IsEmailVerified;
                            result.IsPinCreated = res.Pin == "" ? false : true;
                            result.ContactNumber = res.ContactNumber;
                            result.PhoneExt = res.PhoneExtension;
                            result.MemberId = res.MemberId;
                            result.IsDetailUpdated = res.FirstName != "" ? true : false;
                            result.IsPasswordCreated = res.Password != "" ? true : false;
                            result.RefCode = res.RefCode;
                            result.Token = res.JwtToken;
                            result.Value = Common.EncryptString(res.MemberId.ToString());
                            result.ReponseCode = 1;
                            result.status = true;
                            result.IsCouponUnlocked = IsCouponUnlocked;
                            response.StatusCode = HttpStatusCode.Accepted;
                            response = Request.CreateResponse<Res_UserDetail>(System.Net.HttpStatusCode.OK, result);
                            log.Info($"  {System.DateTime.Now.ToString()}  AddUserController Completed  { Environment.NewLine} ");
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
                log.Error($"  {System.DateTime.Now.ToString()}   AddUserController {ex.ToString()}  { Environment.NewLine} ");
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