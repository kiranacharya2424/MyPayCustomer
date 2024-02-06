using MyPay.API.CustomFrameworkClasses;
using MyPay.API.Models;
using MyPay.API.Models.Request.Voting.Consumer;
using MyPay.API.Models.Request.Voting.Partner;
using MyPay.API.Models.Response.Voting.Partner;
using MyPay.API.Repository;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Get.Events;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using MyPay;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using log4net;
using MyPay.API.Models.Events;
using System.Runtime.Remoting;
using System.Web.Http.Results;
using Newtonsoft.Json;
using Dapper;
using System.Configuration;
using MyPay.API.Models.VotingDB;
using Swashbuckle.Swagger;

namespace MyPay.API.Controllers
{
    public class Voting_V2Controller: MyPayAPIController
    {
        private static ILog log = LogManager.GetLogger(typeof(RequestAPI_EventsController));

        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        VerificationHelper verificationHelper = new VerificationHelper();


        [HttpPost]
        [Route("api/v2/getContestList")]
        public HttpResponseMessage getContestList(VotingContestListReq_C req)
        {
            string UserInput = getRawPostData().Result;

            //Create generic response
            var cres = new CommonResponse();
            var response = Request.CreateResponse<CommonResponse>(HttpStatusCode.BadRequest, cres);

            try
            {
                //Perform generic validation required for all requests in voting controller
                var validationResponse = performGenericValidation(req, UserInput);
                if (validationResponse.StatusCode != HttpStatusCode.OK)
                {
                    return validationResponse;
                }

                string VotingBaseURL = VotingAPISetting_API.voting_BaseURL;
                string VotingEndpoint = "/clientapi/get-contest-list";
                string VotingAPIURL = VotingBaseURL + VotingEndpoint;

                string ApiResponse = string.Empty;


                AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                int VendorApiType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.International_Voting;
                objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse(VotingAPIURL, req.UniqueCustomerId, Common.CreatedBy, Common.CreatedByName, "", "", UserInput, req.DeviceCode, req.PlatForm, VendorApiType, "", "", "", ((int)VendorApi_CommonHelper.VendorTypes.MyPay).ToString());

                VotingContestListResp_P votingContest = new VotingContestListResp_P();
                VotingContestListReq_P partnerReqObject = new VotingContestListReq_P();
                string msg = VotingRepo.processRequest(VotingAPISetting_API.voting_BaseURL, "/clientapi/get-contest-list", objVendor_API_Requests, ref req, ref votingContest, partnerReqObject);

                if (msg.ToLower() != "success")
                {
                    var cres2 = new CommonResponse();
                    cres2.status = false;
                    cres2.ReponseCode = 3;
                    cres2.Message = votingContest.errors[0];
                    var response2 = Request.CreateResponse<CommonResponse>(HttpStatusCode.BadGateway, cres2);

                    return response2;
                }

                ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(msg);
                response = Request.CreateResponse<VotingContestListResp_P>(HttpStatusCode.OK, votingContest);
                votingContest.status = true;

                AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                inobjApiResponse.Id = objVendor_API_Requests.Id;
                AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id > 0)
                {
                    resUpdateRecord.Res_Output = ApiResponse;
                    RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                }
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
                log.Error($"{System.DateTime.Now.ToString()} Get Service Group Events {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



        [HttpPost]
        [Route("api/v2/getSubContestList")]
        public HttpResponseMessage getSubContestList(VotingSubContestListReq_C req)
        {
            string UserInput = getRawPostData().Result;

              req = JsonConvert.DeserializeObject<VotingSubContestListReq_C>(UserInput);

            //Create generic response
            var cres = new CommonResponse();
            var response = Request.CreateResponse<CommonResponse>(HttpStatusCode.BadRequest, cres);

            try
            {
                //Perform generic validation required for all requests in voting controller
                var validationResponse = performGenericValidation(req, UserInput);
                if (validationResponse.StatusCode != HttpStatusCode.OK)
                {
                    return validationResponse;
                }

                string VotingBaseURL = VotingAPISetting_API.voting_BaseURL;
                string VotingEndpoint = "/clientapi/get-subcontest-list";
                string VotingAPIURL = VotingBaseURL + VotingEndpoint;
                string ApiResponse = string.Empty;


                AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                int VendorApiType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.Voting;
               // string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorApiType).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse(VotingAPIURL, req.UniqueCustomerId, Common.CreatedBy, Common.CreatedByName, "", "", UserInput, req.DeviceCode, req.PlatForm, VendorApiType, "", "", "", ((int)VendorApi_CommonHelper.VendorTypes.MyPay).ToString());

                VotingSubContestListResp_P votingSubContest = new VotingSubContestListResp_P();
                VotingSubContestListReq_P partnerReqObject = new VotingSubContestListReq_P();
                string msg = VotingRepo.processRequest(VotingBaseURL, VotingEndpoint, objVendor_API_Requests, ref req, ref votingSubContest, partnerReqObject);

                if (msg.ToLower() != "success")
                {
                    var cres2 = new CommonResponse();
                    cres2.status = false;
                    cres2.ReponseCode = 3;
                    cres2.Message = votingSubContest.errors[0];
                    var response2 = Request.CreateResponse<CommonResponse>(HttpStatusCode.BadGateway, cres2);

                    return response2;
                }

                ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(msg);
                response = Request.CreateResponse<VotingSubContestListResp_P>(HttpStatusCode.OK, votingSubContest);
                votingSubContest.status = true;

                AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                inobjApiResponse.Id = objVendor_API_Requests.Id;
                AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id > 0)
                {
                    resUpdateRecord.Res_Output = ApiResponse;
                    RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                }
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
                log.Error($"{System.DateTime.Now.ToString()} Get Service Group Events {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/v2/getSubContestDetails")]
        public HttpResponseMessage getSubContestDetails(SubContestDetailsReq_C req)
        {
            string UserInput = getRawPostData().Result;

            //Create generic response
            var cres = new CommonResponse();
            var response = Request.CreateResponse<CommonResponse>(HttpStatusCode.BadRequest, cres);

            try
            {
                //Perform generic validation required for all requests in voting controller
                var validationResponse = performGenericValidation(req, UserInput);
                if (validationResponse.StatusCode != HttpStatusCode.OK)
                {
                    return validationResponse;
                }

                string VotingBaseURL = VotingAPISetting_API.voting_BaseURL;
                string VotingEndpoint = "/clientapi/get-contest-details";
                string VotingAPIURL = VotingBaseURL + VotingEndpoint;
                string ApiResponse = string.Empty;

                AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                int VendorApiType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.Voting;
               // string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorApiType).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse(VotingAPIURL, req.UniqueCustomerId, Common.CreatedBy, Common.CreatedByName, "", "", UserInput, req.DeviceCode, req.PlatForm, VendorApiType, "", "", "", ((int)VendorApi_CommonHelper.VendorTypes.MyPay).ToString());

                VotingSubContestDetailsResp_P votingSubContestDetails = new VotingSubContestDetailsResp_P();
                SubContestDetailsReq_C vendorReqObject = new SubContestDetailsReq_C();
                string msg = VotingRepo.processRequest(VotingBaseURL, VotingEndpoint, objVendor_API_Requests, ref req, ref votingSubContestDetails, vendorReqObject);

                if (msg.ToLower() != "success")
                {
                    var cres2 = new CommonResponse();
                    cres2.status = false;
                    cres2.ReponseCode = 3;
                    cres2.Message = votingSubContestDetails.errors[0];
                    var response2 = Request.CreateResponse<CommonResponse>(HttpStatusCode.BadGateway, cres2);

                    return response2;
                }

                ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(msg);
                response = Request.CreateResponse<VotingSubContestDetailsResp_P>(HttpStatusCode.OK, votingSubContestDetails);
                votingSubContestDetails.status = true;

                AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                inobjApiResponse.Id = objVendor_API_Requests.Id;
                AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id > 0)
                {
                    resUpdateRecord.Res_Output = ApiResponse;
                    RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                }
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
                log.Error($"{System.DateTime.Now.ToString()} Get Service Group Events {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/v2/validateVotingCoupon")]
        public HttpResponseMessage validateVotingCoupon(ValidateVotingCouponReq_C req)
        {
            string UserInput = getRawPostData().Result;

            //Create generic response
            var cres = new CommonResponse();
            var response = Request.CreateResponse<CommonResponse>(HttpStatusCode.BadRequest, cres);

            try
            {
                //Perform generic validation required for all requests in voting controller
                var validationResponse = performGenericValidation(req, UserInput);
                if (validationResponse.StatusCode != HttpStatusCode.OK)
                {
                    return validationResponse;
                }

                string VotingBaseURL = VotingAPISetting_API.voting_BaseURL;
                string VotingEndpoint = "/clientapi/validate-promocode";
                string VotingAPIURL = VotingBaseURL + VotingEndpoint;
                string ApiResponse = string.Empty;

                AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                int VendorApiType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.Voting;
                //string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorApiType).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse(VotingAPIURL, req.UniqueCustomerId, Common.CreatedBy, Common.CreatedByName, "", "", UserInput, req.DeviceCode, req.PlatForm, VendorApiType, "", "", "", ((int)VendorApi_CommonHelper.VendorTypes.MyPay).ToString());

                ValidateVotingCouponResp_P couponDetails = new ValidateVotingCouponResp_P();
                ValidateVotingCouponReq_C vendorReqObject = new ValidateVotingCouponReq_C();
                string msg = VotingRepo.processRequest(VotingBaseURL, VotingEndpoint, objVendor_API_Requests, ref req, ref couponDetails, vendorReqObject);

                if (msg.ToLower() != "success")
                {
                    var cres2 = new CommonResponse();
                    cres2.status = false;
                    cres2.ReponseCode = 3;
                    cres2.Message = couponDetails.errors[0];
                    var response2 = Request.CreateResponse<CommonResponse>(HttpStatusCode.BadGateway, cres2);

                    return response2;
                }

                ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(msg);
                response = Request.CreateResponse<ValidateVotingCouponResp_P>(HttpStatusCode.OK, couponDetails);
                couponDetails.status = true;

                AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                inobjApiResponse.Id = objVendor_API_Requests.Id;
                AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id > 0)
                {
                    resUpdateRecord.Res_Output = ApiResponse;
                    RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                }
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
                log.Error($"{System.DateTime.Now.ToString()} Get Service Group Events {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("api/v2/BookVotes")]
        public HttpResponseMessage bookVotes(BookVotesReq_C req)
        {

            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            ILog log2 = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            log.Info("api/v2/BookVotes");
           
            string UserInput = getRawPostData().Result;

            log2.Info("Request: " + UserInput);
            log2.Info("Object: " + req);

            req.createSignature(VotingAPISetting_API.key);

            if (string.IsNullOrEmpty(req.customerEmail)) {
                req.customerEmail = req.MemberId + "@myPayMobile.com";
            }

            log2.Info("Created customer email");


            //Create generic response
            var cres = new CommonResponse();
            var response = Request.CreateResponse<CommonResponse>(HttpStatusCode.BadRequest, cres);

            try
            {
                //Perform generic validation required for all requests in voting controller
                log2.Info("Performing validation");

                var validationResponse = performGenericValidation(req, UserInput);
                if (validationResponse.StatusCode != HttpStatusCode.OK)
                {
                    return validationResponse;
                }

                log2.Info("generic validation passed");


                string VotingBaseURL = VotingAPISetting_API.voting_BaseURL;
                string VotingEndpoint = "/clientapi/vote-request";
                string VotingAPIURL = VotingBaseURL + VotingEndpoint;
                string ApiResponse = string.Empty;

                AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                int VendorApiType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.Voting;
               // string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorApiType).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse(VotingAPIURL, req.UniqueCustomerId, Common.CreatedBy, Common.CreatedByName, "", "", UserInput, req.DeviceCode, req.PlatForm, VendorApiType, "", "", "", ((int)VendorApi_CommonHelper.VendorTypes.MyPay).ToString());

                log2.Info("Trying to book votes");
                BookVotesResp_P voteBookingResp = new BookVotesResp_P();
                BookVotesReq_C vendorReqObject = new BookVotesReq_C();
                string msg = VotingRepo.processRequest(VotingBaseURL, VotingEndpoint, objVendor_API_Requests, ref req, ref voteBookingResp, vendorReqObject);

                if (msg.ToLower() != "success")
                {
                    log2.Info("Votes booking failed");


                    var cres2 = new CommonResponse();
                    cres2.status = false;
                    cres2.ReponseCode = 3;
                    cres2.Message = voteBookingResp.errors[0];
                    var response2 = Request.CreateResponse<CommonResponse>(HttpStatusCode.BadGateway, cres2);

                    return response2;
                }
                log2.Info("Votes booking successful");

                //Vote booking completed. Save to database
                VotingRepo.saveVoteBookingOrder(req, voteBookingResp);

                ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(msg);
                response = Request.CreateResponse<BookVotesResp_P>(HttpStatusCode.OK, voteBookingResp);
                voteBookingResp.status = true;

                log2.Info("Payable amt: " + voteBookingResp.data.payableAmount);
                log2.Info("Order Id: " + voteBookingResp.data.orderId);

                AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                inobjApiResponse.Id = objVendor_API_Requests.Id;
                AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id > 0)
                {
                    resUpdateRecord.Res_Output = ApiResponse;
                    RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                }
                return response;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    log2.Info($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:");

                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                        log2.Info($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");

                    }
                }
                throw;
            }
            catch (Exception ex)
            {

                log2.Info("Error occurred" + ex);
                log.Error($"{System.DateTime.Now.ToString()} Get Service Group Events {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        private void loadVotingSetting(EnvironmentVariableTarget target)
        {
            VotingAPISetting.voting_BaseURL = Environment.GetEnvironmentVariable("Voting_BaseURL", target);
            VotingAPISetting.key = Environment.GetEnvironmentVariable("Voting_Key", target);
            VotingAPISetting.user = Environment.GetEnvironmentVariable("Voting_User", target);
            VotingAPISetting.pass = Environment.GetEnvironmentVariable("Voting_Pass", target);
        }

        [HttpPost]
        [Route("api/v2/BookVotesAndCommit")]
        public HttpResponseMessage BookVotesAndCommit(BookVotesAndConfirmReq_C req)
        {
            loadVotingSetting(EnvironmentVariableTarget.Machine);
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            ILog log2 = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            log.Info("api/v2/BookVotes");


            string UserInput = getRawPostData().Result;

            log2.Info("Request: " + UserInput);
            log2.Info("Object: " + req);

            

            if (string.IsNullOrEmpty(req.customerEmail))
            {
                req.customerEmail = req.MemberId + "@myPayMobile.com";
            }
            req.createSignature(VotingAPISetting.key);

            log2.Info("Created customer email");


            //Create generic response
            var cres = new CommonResponse();
            var response = Request.CreateResponse<CommonResponse>(HttpStatusCode.BadRequest, cres);

            try
            {
                //Perform generic validation required for all requests in voting controller
                log2.Info("Performing validation");

                var validationResponse = performGenericValidation(req, UserInput);
                if (validationResponse.StatusCode != HttpStatusCode.OK)
                {
                    return validationResponse;
                }

                log2.Info("generic validation passed");


                string VotingBaseURL = VotingAPISetting.voting_BaseURL;
                string VotingEndpoint = "/clientapi/vote-request";
                string VotingAPIURL = VotingBaseURL + VotingEndpoint;
                string ApiResponse = string.Empty;

                AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                int VendorApiType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.International_Voting;
                objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse(VotingAPIURL, req.UniqueCustomerId, Common.CreatedBy, Common.CreatedByName, "", "", UserInput, req.DeviceCode, req.PlatForm, VendorApiType, "", "", "", ((int)VendorApi_CommonHelper.VendorTypes.MyPay).ToString());

                log2.Info("Trying to book votes");
                BookVotesResp_P voteBookingResp = new BookVotesResp_P();
                BookVotesReq_P vendorReqObject = new BookVotesReq_P();
                string msg = VotingRepo.processRequest(VotingBaseURL, VotingEndpoint, objVendor_API_Requests, ref req, ref voteBookingResp, vendorReqObject);

                if (msg.ToLower() != "success")
                {
                    log2.Info("Votes booking failed");


                    var cres2 = new CommonResponse();
                    cres2.status = false;
                    cres2.ReponseCode = 3;
                    cres2.Message = voteBookingResp.errors[0];
                    var response2 = Request.CreateResponse<CommonResponse>(HttpStatusCode.BadGateway, cres2);

                    return response2;

                }
                log2.Info("Votes booking successful");

                //Vote booking completed. Save to database
                VotingRepo.saveVoteBookingOrderV2(req, voteBookingResp);

                ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(msg);
                response = Request.CreateResponse<BookVotesResp_P>(HttpStatusCode.OK, voteBookingResp);
                voteBookingResp.status = true;

                log2.Info("Payable amt: " + voteBookingResp.data.payableAmount);
                log2.Info("Order Id: " + voteBookingResp.data.orderId);

                AddVendor_API_Requests outobjApiResponse = new AddVendor_API_Requests();
                GetVendor_API_Requests inobjApiResponse = new GetVendor_API_Requests();
                inobjApiResponse.Id = objVendor_API_Requests.Id;
                AddVendor_API_Requests resUpdateRecord = RepCRUD<GetVendor_API_Requests, AddVendor_API_Requests>.GetRecord(Common.StoreProcedures.sp_VendorAPIRequest_Get, inobjApiResponse, outobjApiResponse);
                if (resUpdateRecord != null && resUpdateRecord.Id != 0 && objVendor_API_Requests.Id > 0)
                {
                    resUpdateRecord.Res_Output = ApiResponse;
                    RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(resUpdateRecord, "vendor_api_requests");
                }

                //NEW CODE ADDED HERE

                //Perform generic validation required for all requests in voting controller
               
                ////Add more validation to the validation below. It's just one part of it. 

                AddUserLoginWithPin user = new AddUserLoginWithPin();


                //Check Token
                string result = new VerificationHelper().getUser(HttpContext.Current.Request.Headers.GetValues("UToken").First(), req.DeviceId, ref user);
                if (result != "success")
                {
                    return createErrorRespWithMessage(Request, result);
                }

                //Check PIN
                result = verificationHelper.checkPin(req.Mpin, user);
                if (result != "success")
                {
                    return createErrorRespWithMessage(Request, result);
                }

                ///////////////////////////
                //Verify user PIN
                ///////////////////////////


                if (result != "success")
                {
                    return createErrorRespWithMessage(Request, result);
                }

                string EventsAPIURL = "api/use-mypay-payments/";
                ApiResponse = string.Empty;
                int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.International_Voting;


                //
                //  Save request sent to vendor in the database and later update and add response to it 
                //
                string authenticationToken = Request.Headers.Authorization.Parameter;
                objVendor_API_Requests = new AddVendor_API_Requests();
                //string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorAPIType).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse(EventsAPIURL, req.Reference, user.MemberId, user.FirstName + " " + user.LastName, "", authenticationToken, UserInput, req.DeviceCode, req.PlatForm, VendorAPIType, "", "", "", ((int)VendorApi_CommonHelper.VendorTypes.MyPay).ToString());


                //
                //  Get vote booking/order from database
                //  
                VotingRepo.VotingOrder booking = VotingRepo.getVoteBookingOrder(voteBookingResp.data.orderId); //req.orderId);
                if (booking == null)
                {
                    return createErrorRespWithMessage(Request, "Vote booking not found");
                }

                CompleteVoteBookingReq_P objectToSendToPartner = new CompleteVoteBookingReq_P();
                objectToSendToPartner = JsonConvert.DeserializeObject<CompleteVoteBookingReq_P>(JsonConvert.SerializeObject(req));

                //
                //  Get the Merchant credentials and create an order to make payment against
                //
                AddMerchant outobject = new AddMerchant();
                GetMerchant inobject = new GetMerchant();
                inobject.MerchantUniqueId = voteBookingResp.data.paymentMerchantId; //user.PaymentMerchantId;
                AddMerchant model = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                VendorApi_CommonHelper.MERCHANT_API_KEY = model.apikey;
                GetVendor_API_Events_CommitMerchant objCommitRes = new GetVendor_API_Events_CommitMerchant();
                string MerchantAPIPassword = Common.DecryptionFromKey(model.API_Password, model.secretkey);
                string msgMerchantPayment = VotingRepo.createMerchantOrder(voteBookingResp.data.orderId, booking.amount.ToString(), voteBookingResp.data.paymentMerchantId, model.UserName, MerchantAPIPassword, req.DeviceId, ref objCommitRes);
                objectToSendToPartner.transactionId = objCommitRes.merchantTransactionId;
                objectToSendToPartner.orderId = voteBookingResp.data.orderId;


                if (msgMerchantPayment.ToLower() == "success")
                {
                    //
                    //  Fetch the merchant order from the database and add user details to it since it only has order ID, merchant ID and amount per se 
                    AddMerchantOrders outobjectOrders = new AddMerchantOrders();
                    GetMerchantOrders inobjectOrders = new GetMerchantOrders();
                    inobjectOrders.MerchantId = req.PaymentMerchantId;
                    inobjectOrders.OrderId = voteBookingResp.data.orderId;
                    AddMerchantOrders resOrders = RepCRUD<GetMerchantOrders, AddMerchantOrders>.GetRecord(Common.StoreProcedures.sp_MerchantOrders_Get, inobjectOrders, outobjectOrders);
                    if (resOrders != null && resOrders.Id != 0)
                    {
                        resOrders.Remarks = $"Voting Payment Pending for Contact no. {user.ContactNumber}";
                        resOrders.Status = (int)AddMerchantOrders.MerchantOrderStatus.Success;
                        resOrders.MemberContactNumber = user.ContactNumber;
                        resOrders.MemberName = user.FirstName + " " + user.LastName;
                        resOrders.MemberId = user.MemberId;
                        resOrders.CurrentBalance = model.MerchantTotalAmount;
                        bool resOrdersFlag = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resOrders, "merchantorders");

                        //
                        //  If order is updated, proceed to debit the user and complete the transaction
                        //
                        if (resOrdersFlag)
                        {
                            GetVendor_API_Event_Payment_Request objPaymentRes = new GetVendor_API_Event_Payment_Request();
                            req.transactionId = objCommitRes.merchantTransactionId;

                            objVendor_API_Requests.Res_Khalti_Id = resOrders.TransactionId;
                            objVendor_API_Requests.Req_ReferenceNo = resOrders.OrderId;
                            objVendor_API_Requests.Req_Khalti_ReferenceNo = resOrders.OrderId;
                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "vendor_api_requests");

                            GetVendor_API_Events_Commit objRes = new GetVendor_API_Events_Commit();
                            Common.authenticationToken = authenticationToken;
                            req.PaymentMode = (!string.IsNullOrEmpty(req.PaymentMode) ? (req.PaymentMode) : "1");
                            req.Reference = resOrders.OrderId;

                            bool IsCouponUnlocked = false;
                            string TransactionID = string.Empty;
                            AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                            string msg2 = VotingRepo.completeVotingPayment(resOrders, voteBookingResp.data.paymentMerchantId, voteBookingResp.data.orderId, req.transactionId, req.PaymentMethodId, booking.amount.ToString(), req.PaymentMode, user, authenticationToken, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, req.BankTransactionID, req.UniqueCustomerId, req.Reference, req.DeviceCode, req.PlatForm, req.MemberId, ref objRes, ref objVendor_API_Requests, objectToSendToPartner);

                            log2.Info("request response: " + msg2);

                            if (objRes.message.ToLower() == "success")
                            {
                                VotingRepo.updateVoteBookingOrder(voteBookingResp.data.orderId, objCommitRes.merchantTransactionId, req.PaymentMethodId.ToString(), true);
                                objRes.status = true;
                                objRes.details = objRes.data.remarks;
                                objRes.TransactionUniqueId = TransactionID;
                                response = Request.CreateResponse<GetVendor_API_Events_Commit>(HttpStatusCode.OK, objRes);

                                log2.Info("Vote completion was successful");
                                log2.Info("Merchant Code" + objRes.data.merchantCode);
                                log2.Info("Order ID" + objRes.data.orderId);
                                log2.Info("Transaction ID" + objRes.data.transactionId);
                                log2.Info("remarks" + objRes.data.remarks);


                                //CREDIT MERCHANT WALLET
                                decimal MerchantBalance = model.MerchantTotalAmount + (resOrders.Amount);
                                resOrders.Remarks = $"Voting Payment Completed for Contact no. {user.ContactNumber}. Merchant Old Bal: {model.MerchantTotalAmount}, Txn Amt: {resOrders.Amount}, New Bal: {MerchantBalance}";
                                resOrders.CurrentBalance = MerchantBalance;
                                resOrders.NetAmount = resOrders.Amount;

                                model.MerchantTotalAmount = MerchantBalance;


                                resOrdersFlag = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resOrders, "merchantorders");
                                bool resFlag = RepCRUD<AddMerchant, GetMerchant>.Update(model, "merchant");

                                return response;
                            }
                            else
                            {
                                log2.Info("Vote completion was NOT SUCCESSFUL");
                            
                                var cres2 = new CommonResponse();
                                cres2.status = false;
                                cres2.ReponseCode = 3;
                                cres2.Message = msg2;
                                var response2 = Request.CreateResponse<CommonResponse>(HttpStatusCode.BadGateway, cres2);

                                return response2;
                            }
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Order not updated");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                        }
                    }
                    else
                    {
                        cres = CommonApiMethod.ReturnBadRequestMessage("Order not found");
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                    }
                }
                else
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage(msgMerchantPayment);
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                }

                ////////////////////////
                //custom code ends here
                ////////////////////////

                return response;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    log2.Info($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:");

                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                        log2.Info($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");

                    }
                }
                throw;
            }
            catch (Exception ex)
            {

                log2.Info("Error occurred" + ex);
                log.Error($"{System.DateTime.Now.ToString()} Get Service Group Events {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        [Route("api/v2/ConfirmVotingOrder")]
        public HttpResponseMessage ConfirmVotingOrder(CompleteVoteBookingReq_C req)
        {

            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            ILog log2 = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            string UserInput = getRawPostData().Result;
            log2.Info("api/v2/ConfirmVotingOrder");
            log2.Info("Request: " + UserInput);
            log2.Info("Object: " + req);

            CompleteVoteBookingReq_P objectToSendToPartner = new CompleteVoteBookingReq_P();
            objectToSendToPartner = JsonConvert.DeserializeObject<CompleteVoteBookingReq_P>(JsonConvert.SerializeObject(req));


            //Create generic response
            var cres = new CommonResponse();
            var response = Request.CreateResponse<CommonResponse>(HttpStatusCode.BadRequest, cres);

            try
            {
                //Perform generic validation required for all requests in voting controller
                var validationResponse = performGenericValidation(req, UserInput, true);
                if (validationResponse.StatusCode != HttpStatusCode.OK)
                {
                    return validationResponse;
                }

                ////////////////////////
                //custom code starts here
                ////////////////////////

                AddUserLoginWithPin user = new AddUserLoginWithPin();
                
                //Check Token
                string result = new VerificationHelper().getUser(HttpContext.Current.Request.Headers.GetValues("UToken").First(), req.DeviceId, ref user);
                if ( result != "success")
                {
                    return createErrorRespWithMessage(Request, result);
                }

                //Check PIN
                result = verificationHelper.checkPin(req.Mpin, user);
                if (result != "success")
                {
                    return createErrorRespWithMessage(Request, result);
                }

                ///////////////////////////
                //Verify user PIN
                ///////////////////////////
                ///
                if (result != "success") {
                   return createErrorRespWithMessage(Request, result);
                }

                string EventsAPIURL = "api/use-mypay-payments/";
                string ApiResponse = string.Empty;
                int VendorAPIType = (int)VendorApi_CommonHelper.KhaltiAPIName.International_Voting;

                //
                //  Save request sent to vendor in the database and later update and add response to it 
                //
                string authenticationToken = Request.Headers.Authorization.Parameter;
                AddVendor_API_Requests objVendor_API_Requests = new AddVendor_API_Requests();
                //string VendorApiTypeName = @Enum.GetName(typeof(MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName), VendorAPIType).ToString().ToUpper().Replace("_", " ").Replace("KHALTI", " ").ToString();
                objVendor_API_Requests = VendorApi_CommonHelper.SendDataToVendor_SaveResponse(EventsAPIURL, req.Reference, user.MemberId, user.FirstName + " " + user.LastName, "", authenticationToken, UserInput, req.DeviceCode, req.PlatForm, VendorAPIType, "", "", "", ((int)VendorApi_CommonHelper.VendorTypes.MyPay).ToString());

                //
                //  Get vote booking/order from database
                //  
                VotingRepo.VotingOrder booking = VotingRepo.getVoteBookingOrder(req.orderId);
                if (booking == null) {
                    return createErrorRespWithMessage(Request, "Vote booking not found");
                }

                //
                //  Get the Merchant credentials and create an order to make payment against
                //
                AddMerchant outobject = new AddMerchant();
                GetMerchant inobject = new GetMerchant();
                inobject.MerchantUniqueId = req.PaymentMerchantId; //user.PaymentMerchantId;
                AddMerchant model = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                VendorApi_CommonHelper.MERCHANT_API_KEY = model.apikey;
                GetVendor_API_Events_CommitMerchant objCommitRes = new GetVendor_API_Events_CommitMerchant();
                string MerchantAPIPassword = Common.DecryptionFromKey(model.API_Password, model.secretkey);
                //string msgMerchantPayment = RepEvents.RequestServiceGroup_Events_CommitMerchantTransactions(req.orderId, "get amount from order id and put here", req.PaymentMerchantId, model.UserName, MerchantAPIPassword, req.DeviceId, ref objCommitRes);
                string msgMerchantPayment = VotingRepo.createMerchantOrder (req.orderId, booking.amount.ToString(), req.PaymentMerchantId, model.UserName, MerchantAPIPassword, req.DeviceId, ref objCommitRes);
                objectToSendToPartner.transactionId = objCommitRes.merchantTransactionId;
                

                if (msgMerchantPayment.ToLower() == "success")
                {
                    //
                    //  Fetch the merchant order from the database and add user details to it since it only has order ID, merchant ID and amount per se 
                    AddMerchantOrders outobjectOrders = new AddMerchantOrders();
                    GetMerchantOrders inobjectOrders = new GetMerchantOrders();
                    inobjectOrders.MerchantId = req.PaymentMerchantId;
                    inobjectOrders.OrderId = req.orderId;
                    AddMerchantOrders resOrders = RepCRUD<GetMerchantOrders, AddMerchantOrders>.GetRecord(Common.StoreProcedures.sp_MerchantOrders_Get, inobjectOrders, outobjectOrders);
                    if (resOrders != null && resOrders.Id != 0)
                    {
                        resOrders.Remarks = $"Voting Payment Pending for Contact no. {user.ContactNumber}";
                        resOrders.Status = (int)AddMerchantOrders.MerchantOrderStatus.Success;
                        resOrders.MemberContactNumber = user.ContactNumber;
                        resOrders.MemberName = user.FirstName + " " + user.LastName;
                        resOrders.MemberId = user.MemberId;
                        resOrders.CurrentBalance = model.MerchantTotalAmount;
                        bool resOrdersFlag = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resOrders, "merchantorders");

                        //
                        //  If order is updated, proceed to debit the user and complete the transaction
                        //
                        if (resOrdersFlag)
                        {
                            GetVendor_API_Event_Payment_Request objPaymentRes = new GetVendor_API_Event_Payment_Request();
                            req.transactionId = objCommitRes.merchantTransactionId;

                            objVendor_API_Requests.Res_Khalti_Id = resOrders.TransactionId;
                            objVendor_API_Requests.Req_ReferenceNo = resOrders.OrderId;
                            objVendor_API_Requests.Req_Khalti_ReferenceNo = resOrders.OrderId;
                            RepCRUD<AddVendor_API_Requests, GetVendor_API_Requests>.Update(objVendor_API_Requests, "vendor_api_requests");

                            GetVendor_API_Events_Commit objRes = new GetVendor_API_Events_Commit();
                            Common.authenticationToken = authenticationToken;
                            req.PaymentMode = (!string.IsNullOrEmpty(req.PaymentMode) ? (req.PaymentMode) : "1");
                            req.Reference = resOrders.OrderId;

                            bool IsCouponUnlocked = false; 
                            string TransactionID = string.Empty;
                            AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                            string msg2 = VotingRepo.completeVotingPayment(resOrders, req.PaymentMerchantId, req.orderId, req.transactionId, req.PaymentMethodId, booking.amount.ToString(), req.PaymentMode, user, authenticationToken, ref IsCouponUnlocked, ref TransactionID, resGetCouponsScratched, req.BankTransactionID, req.UniqueCustomerId, req.Reference, req.DeviceCode, req.PlatForm, req.MemberId, ref objRes, ref objVendor_API_Requests, objectToSendToPartner);

                            log2.Info("request response: " + msg2);

                            if (objRes.message.ToLower() == "success")
                            {
                                VotingRepo.updateVoteBookingOrder(req.orderId, objCommitRes.merchantTransactionId, req.PaymentMethodId.ToString(), true);
                                objRes.status = true;
                                objRes.details = objRes.data.remarks;
                                response = Request.CreateResponse<GetVendor_API_Events_Commit>(HttpStatusCode.OK,objRes);

                                log2.Info("Vote completion was successful");
                                log2.Info("Merchant Code" + objRes.data.merchantCode);
                                log2.Info("Order ID" + objRes.data.orderId);
                                log2.Info("Transaction ID" + objRes.data.transactionId);
                                log2.Info("remarks" + objRes.data.remarks);

                                //CREDIT MERCHANT WALLET
                                decimal MerchantBalance = model.MerchantTotalAmount + (resOrders.Amount);
                                resOrders.Remarks = $"Voting Payment Completed for Contact no. {user.ContactNumber}. Merchant Old Bal: {model.MerchantTotalAmount}, Txn Amt: {resOrders.Amount}, New Bal: {MerchantBalance}";
                                resOrders.CurrentBalance = MerchantBalance;
                                resOrders.NetAmount = resOrders.Amount;

                                model.MerchantTotalAmount = MerchantBalance;


                                resOrdersFlag = RepCRUD<AddMerchantOrders, GetMerchantOrders>.Update(resOrders, "merchantorders");
                                bool resFlag = RepCRUD<AddMerchant, GetMerchant>.Update(model, "merchant");

                                return response;
                            }
                            else
                            {
                                log2.Info("Vote completion was NOT SUCCESSFUL");
                                log2.Info("Merchant Code" + objRes.data.merchantCode);
                                log2.Info("Order ID" + objRes.data.orderId);
                                log2.Info("Transaction ID" + objRes.data.transactionId);
                                log2.Info("remarks" + objRes.data.remarks);
                                
                                var cres2 = new CommonResponse();
                                cres2.status = false;
                                cres2.ReponseCode = 3;
                                cres2.Message = msg2;
                                var response2 = Request.CreateResponse<CommonResponse>(HttpStatusCode.BadGateway, cres2);

                                return response2;
                            }
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Order not updated");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                        }
                    }
                    else
                    {
                        cres = CommonApiMethod.ReturnBadRequestMessage("Order not found");
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                        ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                    }
                }
                else
                {
                    cres = CommonApiMethod.ReturnBadRequestMessage(msgMerchantPayment);
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                }

                ////////////////////////
                //custom code ends here
                ////////////////////////
                
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
                log.Error($"{System.DateTime.Now.ToString()} Get Service Group Events {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
