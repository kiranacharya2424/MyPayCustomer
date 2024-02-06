using log4net;
using MyPay.API.Models;
using MyPay.API.Models.Voting;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class GetVotingCompetitionController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(GetVotingCompetitionController));
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/getvotingcompetition")]
        public HttpResponseMessage GetVoteCompetition(Req_VotingCompetition user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside GetVoteCompetition" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            List<AddVotingCompetition> list = new List<AddVotingCompetition>();
            Res_VotingCompetition result = new Res_VotingCompetition();
            var response = Request.CreateResponse<Res_VotingCompetition>(System.Net.HttpStatusCode.BadRequest, result);
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
                        list = new List<AddVotingCompetition>();
                        AddVotingCompetition outobject = new AddVotingCompetition();
                        GetVotingCompetition inobject = new GetVotingCompetition();
                        inobject.CheckDelete = 0;
                        inobject.CheckActive = 1;
                        string WebPrefix = Common.LiveSiteUrl;
                        //inobject.PublishTime =Convert.ToDateTime(DateTime.UtcNow).ToString("dd-MMM-yyyy hh:mm:ss");                       
                        list = RepCRUD<GetVotingCompetition, AddVotingCompetition>.GetRecordList("sp_VotingCompetition_Get", inobject, outobject);
                        foreach (var item in list)
                        {
                            item.Image = WebPrefix + "Images/VotingCompetition/" + item.Image;
                        }
                        list = list.OrderBy(c => c.Order).ToList();
                        result.status = true;
                        result.data = list;
                        result.Message = "Success";
                        result.ReponseCode = 1;
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} GetVoteCompetition completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} GetVoteCompetition {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}