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
using MyPay.API.Models.Request;
using MyPay.API.Models.Response;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Repository;

namespace MyPay.API.Controllers
{
    public class GetUserDetailController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(GetUserDetailController));
        public HttpResponseMessage Post(Req_GetUserDetail user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside GetUserDetailController" + Environment.NewLine);
            string WebPrefix = Common.LiveSiteUrl;
            Res_GetUserDetail result = new Res_GetUserDetail();
            var response = Request.CreateResponse<Res_GetUserDetail>(System.Net.HttpStatusCode.BadRequest, result);

            var userInput = getRawPostData().Result;

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
                    string md5hash = Common.getHashMD5(userInput); //Common.CheckHash(user);

                    string results = new CommonHelpers().CheckApiToken(user.Hash, user.TimeStamp, md5hash, user.PlatForm, user.Version, user.DeviceCode, user.SecretKey);
                    if (results.ToLower() != "success")
                    {
                        CommonResponse cres = CommonApiMethod.ReturnBadRequestMessage(results);
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        return response;
                    }
                    string CommonResult = "";
                    AddUser res = new AddUser();
                    AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                    if (!string.IsNullOrEmpty(user.MemberId.ToString()) && user.MemberId.ToString() != "0")
                    {
                        Int64 memId = Convert.ToInt64(user.MemberId);
                        int VendorAPIType = 0;
                        int Type = 0;
                        res = new CommonHelpers().CheckUserDetailOld(ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
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

                        result.Name = res.FirstName + " " + res.MiddleName + " " + res.LastName;
                        result.EmailId = res.Email;
                        result.Gender = res.Gender;
                        result.IsBankAdded = res.IsBankAdded > 0;
                        result.IsKycVerified = res.IsKYCApproved;
                        result.MemberId = res.MemberId;
                        result.Value = Common.EncryptString(res.MemberId.ToString());
                        result.IsDetailUpdated = res.FirstName != "" ? true : false;
                        result.IsPasswordCreated = res.Password != "" ? true : false;
                        result.IsPhoneVerified = res.IsPhoneVerified;
                        result.IsEmailVerified = res.IsEmailVerified;
                        result.IsPinCreated = res.Pin == "" ? false : true;
                        result.ContactNumber = res.ContactNumber;
                        result.PhoneExt = res.PhoneExtension;
                        result.IsDetailUpdated = res.FirstName != "" ? true : false;
                        result.IsPasswordCreated = res.Password != "" ? true : false;
                        result.UserId = res.UserId;
                        result.Address = res.Address;
                        result.StateId = res.StateId;
                        result.State = res.State;
                        result.DistrictId = res.DistrictId;
                        result.District = res.City;
                        result.MunicipalityId = res.MunicipalityId;
                        result.Municipality = res.Municipality;
                        result.StreetName = res.StreetName;
                        result.HouseNumber = res.HouseNumber;
                        result.IsYourPermanentAndTemporaryAddressSame = res.IsYourPermanentAndTemporaryAddressSame;
                        result.MeritalStatus = res.MeritalStatus;
                        result.CountryId = res.CountryId;
                        result.CountryName = res.CountryName;
                        result.DateofBirth = res.DateofBirth;
                        result.FatherName = res.FatherName;
                        result.GrandFatherName = res.GrandFatherName;
                        result.NationalIdProofBack = string.IsNullOrEmpty(res.NationalIdProofBack) ? "" : (WebPrefix + "UserDocuments/Images/" + res.NationalIdProofBack);
                        result.NationalIdProofFront = string.IsNullOrEmpty(res.NationalIdProofFront) ? "" : (WebPrefix + "UserDocuments/Images/" + res.NationalIdProofFront);
                        result.Nationality = res.Nationality;
                        result.Occupation = res.EmployeeType.ToString();
                        result.ProofType = res.ProofType;
                        result.TotalAmount = (float)res.TotalAmount;
                        result.RefCode = res.RefCode;
                        result.RefId = res.RefId.ToString();
                        //result.Pin = Common.DecryptString(res.Pin.ToString());
                        result.UserImage = string.IsNullOrEmpty(res.UserImage) ? "" : (WebPrefix + "UserDocuments/Images/" + res.UserImage);
                        result.WardNumber = res.WardNumber;
                        result.ZipCode = res.ZipCode;
                        result.FirstName = res.FirstName;
                        result.MiddleName = res.MiddleName;
                        result.LastName = res.LastName;
                        result.IssuedBy = res.IssuedBy;
                        result.DOBType = res.DOBType;
                        result.DocumentNumber = res.DocumentNumber;
                        result.CurrentStateId = res.CurrentStateId;
                        result.CurrentState = res.CurrentState;
                        result.CurrentDistrictId = res.CurrentDistrictId;
                        result.CurrentDistrict = res.CurrentDistrict;
                        result.CurrentMunicipalityId = res.CurrentMunicipalityId;
                        result.CurrentMunicipality = res.CurrentMunicipality;
                        result.CurrentStreetName = res.CurrentStreetName;
                        result.CurrentWardNumber = res.CurrentWardNumber;
                        result.CurrentHouseNumber = res.CurrentHouseNumber;
                        result.ExpiryDate = res.ExpiryDate;
                        result.IssueDate = res.IssueDate;
                        result.IssueFromDistrictID = res.IssueFromDistrictID;
                        result.IssueFromDistrictName = res.IssueFromDistrictName;
                        result.IssueFromStateID = res.IssueFromStateID;
                        result.IssueFromStateName = res.IssueFromStateName;
                        result.AlternateContactNumber = res.AlternateContactNumber;
                        result.MotherName = res.MotherName;
                        result.SpouseName = res.SpouseName;
                        result.EnablePushNotification = res.EnablePushNotification;
                        AddOccupation outobject_occ = new AddOccupation();
                        GetOccupation inobject_occ = new GetOccupation();
                        inobject_occ.Id = res.EmployeeType;
                        AddOccupation res_occ = RepCRUD<GetOccupation, AddOccupation>.GetRecord(Common.StoreProcedures.sp_Occupation_Get, inobject_occ, outobject_occ);
                        if (res_occ != null && res_occ.Id != 0)
                        {
                            result.OccupationName = res_occ.CategoryName;
                        }
                        result.Message = "Success";
                        result.ReponseCode = 1;
                        result.status = true;
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<Res_GetUserDetail>(System.Net.HttpStatusCode.OK, result);
                        log.Info($"{System.DateTime.Now.ToString()} GetUserDetailController completed" + Environment.NewLine);
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
            catch (Exception ex)
            {
                log.Error($"{System.DateTime.Now.ToString()} GetUserDetailController {ex.ToString()} " + Environment.NewLine);
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