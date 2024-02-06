using log4net;
using MyPay.API.Models;
using MyPay.API.Models.Voting;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class GetVotingPackagesController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(GetVotingPackagesController));
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/GetVotingPackages")]
        public HttpResponseMessage GetVotingPackages(Req_VotingPackages user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside GetVotingPackages" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            List<AddVotingPackages> list = new List<AddVotingPackages>();
            Res_VotingPackages result = new Res_VotingPackages();
            var response = Request.CreateResponse<Res_VotingPackages>(System.Net.HttpStatusCode.BadRequest, result);
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
                        AddUserLoginWithPin resGetRecord = new AddUserLoginWithPin();
                        int VendorAPIType = 0;
                        int Type = 0;
                        Int64 memId = 0;
                        AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                        resGetRecord = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "", false, user.Mpin, "", true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }

                        if (user.VotingCompetitionID == 0 || user.VotingCompetitionID == null)
                        {


                            cres = CommonApiMethod.ReturnBadRequestMessage("Please send VotingCompetitionID");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            return response;
                        }
                        list = new List<AddVotingPackages>();
                        AddVotingPackages outobject = new AddVotingPackages();
                        GetVotingPackages inobject = new GetVotingPackages();
                        inobject.CheckDelete = 0;
                        inobject.CheckActive = 1;
                        inobject.VotingCompetitionID = user.VotingCompetitionID;
                        list = RepCRUD<GetVotingPackages, AddVotingPackages>.GetRecordList("sp_VotingPackages_Get", inobject, outobject);
                        result.status = true;
                        result.data = list;
                        result.Message = "Success";
                        result.ReponseCode = 1;
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} GetVotingPackages completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} GetVotingPackages {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}