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
    public class GetVotingCandidatesController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(GetVotingCandidatesController));

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/GetVotingcCandidates")]
        public HttpResponseMessage GetVotingCandidates(Req_VotingCandidates user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside GetVotingCandidates" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            List<AddVotingCandidate> list = new List<AddVotingCandidate>();
            Res_VotingCandidates result = new Res_VotingCandidates();
            var response = Request.CreateResponse<Res_VotingCandidates>(System.Net.HttpStatusCode.BadRequest, result);
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
                        res = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true, true);
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

                        list = new List<AddVotingCandidate>();
                        AddVotingCandidate outobject = new AddVotingCandidate();
                        GetVotingCandidate inobject = new GetVotingCandidate();
                        inobject.CheckDelete = 0;
                        inobject.CheckActive = 1;
                        inobject.VotingCompetitionID = user.VotingCompetitionID;
                        list = RepCRUD<GetVotingCandidate, AddVotingCandidate>.GetRecordList("sp_VotingCandidate_Get", inobject, outobject);
                        string WebPrefix = Common.LiveSiteUrl;
                        foreach (var item in list)
                        {
                            item.ContactNo = "";
                            item.EmailID = "";
                            item.Address = "";
                            item.City = "";
                            item.State = "";
                            item.CountryName = "";
                            item.ZipCode = 0;
                            item.TotalAmount = 0;
                            item.TotalVotes = 0;
                            item.FreeVotes = 0;
                            item.Rank = 0;
                            item.Image = WebPrefix + "Images/VotingCandidate/" + item.Image;
                        }
                        result.status = true;
                        result.data = list;
                        result.Message = "Success";
                        result.ReponseCode = 1;
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} GetVotingCandidates completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} GetVotingCandidates {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}