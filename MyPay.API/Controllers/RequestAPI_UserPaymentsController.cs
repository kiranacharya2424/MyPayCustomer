using log4net;
using MyPay.API.Models;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class RequestAPI_UserPaymentsController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(RequestAPI_UserPaymentsController));
        string ApiResponse = string.Empty;


        [HttpPost]
        [Route("api/add-user-payment")]
        public HttpResponseMessage GetServiceGroupAddPayment(Req_API_Add_Payment_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside  Service Group Add Payment" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_API_Add_Payments_Requests result = new Res_API_Add_Payments_Requests();
            var response = Request.CreateResponse<Res_API_Add_Payments_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        if (!string.IsNullOrEmpty(user.MemberID.ToString()) && user.MemberID.ToString() != "0")
                        {
                            string msg = ValidatePayment(user);

                            if (msg == "")
                            {
                                string CommonResult = "";
                                AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                                AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                                Int64 memId = Convert.ToInt64(user.MemberID);
                                int VendorAPIType = 0;
                                int Type = 0;
                                resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
                                if (CommonResult.ToLower() != "success")
                                {
                                    CommonResponse cres1 = new CommonResponse();
                                    cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                                    return response;
                                }

                                AddUserSavedPayments addUserSavedPayments = new AddUserSavedPayments();
                                addUserSavedPayments.ServiceID = user.ServiceID;
                                addUserSavedPayments.MobileNumber = user.MobileNumber;
                                addUserSavedPayments.Amount = user.Amount;
                                addUserSavedPayments.SubscriberID = user.SubscriberID;
                                addUserSavedPayments.CasID = user.CasID;
                                addUserSavedPayments.PackageID = user.PackageID;
                                addUserSavedPayments.PackageName = user.PackageName;
                                addUserSavedPayments.CustomerID = user.CustomerID;
                                addUserSavedPayments.CustomerName = user.CustomerName;
                                addUserSavedPayments.OldWardNumber = user.OldWardNumber;
                                addUserSavedPayments.STB = user.STB;
                                addUserSavedPayments.UserName = user.UserName;
                                addUserSavedPayments.FullName = user.FullName;
                                addUserSavedPayments.LandlineNumber = user.LandlineNumber;
                                addUserSavedPayments.Address = user.Address;
                                addUserSavedPayments.CounterID = user.CounterID;
                                addUserSavedPayments.CounterName = user.CounterName;
                                addUserSavedPayments.ScNumber = user.ScNumber;
                                addUserSavedPayments.ConsumerID = user.ConsumerID;
                                addUserSavedPayments.SubscriptionID = user.SubscriptionID;
                                addUserSavedPayments.SubscriptionName = user.SubscriptionName;
                                addUserSavedPayments.AcceptanceNo = user.AcceptanceNo;
                                addUserSavedPayments.PolicyNumber = user.PolicyNumber;
                                addUserSavedPayments.DateOfBirth = user.DateOfBirth;
                                addUserSavedPayments.InsuranceID = user.InsuranceID;
                                addUserSavedPayments.InsuranceName = user.InsuranceName;
                                addUserSavedPayments.DebitNoteNo = user.DebitNoteNo;
                                addUserSavedPayments.Email = user.Email;
                                addUserSavedPayments.PolicyType = user.PolicyType;
                                addUserSavedPayments.Branch = user.Branch;
                                addUserSavedPayments.PolicyCategory = user.PolicyCategory;
                                addUserSavedPayments.PolicyDescription = user.PolicyDescription;
                                addUserSavedPayments.ChitNumber = user.ChitNumber;
                                addUserSavedPayments.FiscalYearID = user.FiscalYearID;
                                addUserSavedPayments.FiscalYearValue = user.FiscalYearValue;
                                addUserSavedPayments.ProvinceID = user.ProvinceID;
                                addUserSavedPayments.ProvinceName = user.ProvinceName;
                                addUserSavedPayments.DistrictID = user.DistrictID;
                                addUserSavedPayments.DistrictValue = user.DistrictValue;
                                addUserSavedPayments.BankID = user.BankID;
                                addUserSavedPayments.BankName = user.BankName;
                                addUserSavedPayments.CreditCardNumber = user.CreditCardNumber;
                                addUserSavedPayments.CreditCardOwner = user.CreditCardOwner;
                                addUserSavedPayments.MemberID = user.MemberID;
                                addUserSavedPayments.PaymentName = user.PaymentName;
                                addUserSavedPayments.IsSchedulePayment = user.IsSchedulePayment;
                                addUserSavedPayments.ScheduleDate = user.ScheduleDate;
                                addUserSavedPayments.PaymentCycle = user.PaymentCycle;
                                //addUserSavedPayments.CreatedBy  = user.MemberID;
                                // addUserSavedPayments.CreatedByName  = user.MemberID;

                                //addUserSavedPayments.CreatedDate = user.MemberID;
                                //addUserSavedPayments.UpdatedDate= user.MemberID;
                                //addUserSavedPayments.UpdatedBy  = user.MemberID;
                                //addUserSavedPayments.UpdatedByName = user.MemberID;

                                addUserSavedPayments.IsApprovedByAdmin = true;
                                addUserSavedPayments.IsActive = true;




                                if (addUserSavedPayments.Add())
                                {
                                    // result.data = objEvents;
                                    result.ReponseCode = 1;
                                    result.status = true;
                                    result.Message = "success";
                                    response = Request.CreateResponse<Res_API_Add_Payments_Requests>(System.Net.HttpStatusCode.OK, result);
                                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                                }
                                else
                                {
                                    cres = CommonApiMethod.ReturnBadRequestMessage("User Payment" + " not saved");
                                    response.StatusCode = HttpStatusCode.BadRequest;
                                    response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                    ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                                }
                            }
                            else
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage(msg);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);

                            }
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Member Id not found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                        }
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} Service Group Add Payment completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} Service Group Add Payment {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPost]
        [Route("api/get-user-payment")]
        public HttpResponseMessage GetServiceGroupUserPayment(Req_API_Get_Payment_Requests user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside  Service Group User Payment" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            Res_API_Get_Payments_Requests result = new Res_API_Get_Payments_Requests();
            var response = Request.CreateResponse<Res_API_Get_Payments_Requests>(System.Net.HttpStatusCode.BadRequest, result);
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
                        if (!string.IsNullOrEmpty(user.MemberID.ToString()) && user.MemberID.ToString() != "0")
                        {

                            string CommonResult = "";
                            AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                            AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                            Int64 memId = Convert.ToInt64(user.MemberID);
                            int VendorAPIType = 0;
                            int Type = 0;
                            resuserdetails = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", false, true);
                            if (CommonResult.ToLower() != "success")
                            {
                                CommonResponse cres1 = new CommonResponse();
                                cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                                return response;
                            }
                            List<AddUserSavedPayments> objPaymentList = new List<AddUserSavedPayments>();
                            AddUserSavedPayments w = new AddUserSavedPayments();
                            w.MemberID = user.MemberID;
                            w.ServiceID = user.ServiceID;
                            w.CheckActive = 1;
                            w.CheckDelete = 0;
                            DataTable dt = w.GetList();
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    AddUserSavedPayments obj = new AddUserSavedPayments();
                                    obj.ServiceID = Convert.ToInt32(dt.Rows[i]["ServiceID"].ToString());
                                    obj.MobileNumber = Convert.ToString(dt.Rows[i]["MobileNumber"].ToString());
                                    obj.Amount = Convert.ToDecimal(dt.Rows[i]["Amount"].ToString());
                                    obj.SubscriberID = Convert.ToString(dt.Rows[i]["SubscriberID"].ToString());
                                    obj.CasID = Convert.ToString(dt.Rows[i]["CasID"].ToString());
                                    obj.PackageID = Convert.ToString(dt.Rows[i]["PackageID"].ToString());
                                    obj.PackageName = Convert.ToString(dt.Rows[i]["PackageName"].ToString());
                                    obj.CustomerID = Convert.ToString(dt.Rows[i]["CustomerID"].ToString());
                                    obj.CustomerName = Convert.ToString(dt.Rows[i]["CustomerName"].ToString());
                                    obj.OldWardNumber = Convert.ToString(dt.Rows[i]["OldWardNumber"].ToString());
                                    obj.STB = Convert.ToString(dt.Rows[i]["STB"].ToString());
                                    obj.UserName = Convert.ToString(dt.Rows[i]["UserName"].ToString());
                                    obj.FullName = Convert.ToString(dt.Rows[i]["FullName"].ToString());
                                    obj.LandlineNumber = Convert.ToString(dt.Rows[i]["LandlineNumber"].ToString());
                                    obj.Address = Convert.ToString(dt.Rows[i]["Address"].ToString());
                                    obj.CounterID = Convert.ToString(dt.Rows[i]["CounterID"].ToString());
                                    obj.CounterName = Convert.ToString(dt.Rows[i]["CounterName"].ToString());
                                    obj.ScNumber = Convert.ToString(dt.Rows[i]["ScNumber"].ToString());
                                    obj.ConsumerID = Convert.ToString(dt.Rows[i]["ConsumerID"].ToString());
                                    obj.SubscriptionID = Convert.ToString(dt.Rows[i]["SubscriptionID"].ToString());
                                    obj.SubscriptionName = Convert.ToString(dt.Rows[i]["SubscriptionName"].ToString());
                                    obj.AcceptanceNo = Convert.ToString(dt.Rows[i]["AcceptanceNo"].ToString());
                                    obj.PolicyNumber = Convert.ToString(dt.Rows[i]["PolicyNumber"].ToString());
                                    obj.DateOfBirth = Convert.ToString(dt.Rows[i]["DateOfBirth"].ToString());
                                    obj.InsuranceID = Convert.ToString(dt.Rows[i]["InsuranceID"].ToString());
                                    obj.InsuranceName = Convert.ToString(dt.Rows[i]["InsuranceName"].ToString());
                                    obj.DebitNoteNo = Convert.ToString(dt.Rows[i]["DebitNoteNo"].ToString());
                                    obj.Email = Convert.ToString(dt.Rows[i]["Email"].ToString());
                                    obj.PolicyType = Convert.ToString(dt.Rows[i]["PolicyType"].ToString());
                                    obj.Branch = Convert.ToString(dt.Rows[i]["Branch"].ToString());
                                    obj.PolicyCategory = Convert.ToString(dt.Rows[i]["PolicyCategory"].ToString());
                                    obj.PolicyDescription = Convert.ToString(dt.Rows[i]["PolicyDescription"].ToString());
                                    obj.ChitNumber = Convert.ToString(dt.Rows[i]["ChitNumber"].ToString());
                                    obj.FiscalYearID = Convert.ToString(dt.Rows[i]["FiscalYearID"].ToString());
                                    obj.FiscalYearValue = Convert.ToString(dt.Rows[i]["FiscalYearValue"].ToString());
                                    obj.ProvinceID = Convert.ToString(dt.Rows[i]["ProvinceID"].ToString());
                                    obj.ProvinceName = Convert.ToString(dt.Rows[i]["ProvinceName"].ToString());
                                    obj.DistrictID = Convert.ToString(dt.Rows[i]["DistrictID"].ToString());
                                    obj.DistrictValue = Convert.ToString(dt.Rows[i]["DistrictValue"].ToString());
                                    obj.BankID = Convert.ToString(dt.Rows[i]["BankID"].ToString());
                                    obj.BankName = Convert.ToString(dt.Rows[i]["BankName"].ToString());
                                    obj.CreditCardNumber = Convert.ToString(dt.Rows[i]["CreditCardNumber"].ToString());
                                    obj.CreditCardOwner = Convert.ToString(dt.Rows[i]["CreditCardOwner"].ToString());
                                    obj.MemberID = Convert.ToInt64(dt.Rows[i]["MemberID"].ToString());
                                    obj.PaymentName = Convert.ToString(dt.Rows[i]["PaymentName"].ToString());
                                    obj.IsSchedulePayment = Convert.ToInt32(dt.Rows[i]["IsSchedulePayment"].ToString());
                                    obj.ScheduleDate = Convert.ToDateTime(dt.Rows[i]["ScheduleDate"].ToString());
                                    obj.PaymentCycle = Convert.ToInt32(dt.Rows[i]["PaymentCycle"].ToString());
                                    obj.CreatedBy = Convert.ToInt64(dt.Rows[i]["CreatedBy"].ToString());
                                    obj.CreatedByName = Convert.ToString(dt.Rows[i]["CreatedByName"].ToString());
                                    obj.Sno = Convert.ToString(dt.Rows[i]["Sno"].ToString());
                                    obj.CreatedDate = Convert.ToDateTime(dt.Rows[i]["CreatedDate"].ToString());
                                    obj.UpdatedDate = Convert.ToDateTime(dt.Rows[i]["UpdatedDate"].ToString());
                                    obj.UpdatedBy = Convert.ToInt64(dt.Rows[i]["UpdatedBy"].ToString());
                                    obj.UpdatedByName = Convert.ToString(dt.Rows[i]["UpdatedByName"].ToString());
                                    obj.IsDeleted = Convert.ToBoolean(dt.Rows[i]["IsDeleted"].ToString());
                                    obj.IsApprovedByAdmin = Convert.ToBoolean(dt.Rows[i]["IsApprovedByAdmin"].ToString());
                                    obj.IsActive = Convert.ToBoolean(dt.Rows[i]["IsActive"].ToString());
                                    obj.Id = Convert.ToInt64(dt.Rows[i]["Id"].ToString());
                                    objPaymentList.Add(obj);
                                }
                                result.data = objPaymentList;
                                result.ReponseCode = 1;
                                result.status = true;
                                result.Message = "success";
                                response = Request.CreateResponse<Res_API_Get_Payments_Requests>(System.Net.HttpStatusCode.OK, result);
                                ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                            }
                            else
                            {
                                cres = CommonApiMethod.ReturnBadRequestMessage("No Payment Found");
                                response.StatusCode = HttpStatusCode.BadRequest;
                                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                                ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                            }
                        }
                        else
                        {
                            cres = CommonApiMethod.ReturnBadRequestMessage("Member Id not found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                            ApiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(cres);
                        }



                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} Service Group Get User Payment completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} Service Group Get User Payment {ex.ToString()} " + Environment.NewLine);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }


        }

        public string ValidatePayment(Req_API_Add_Payment_Requests user)
        {

            string msg = "";
            if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_topup)
            {
                if (string.IsNullOrEmpty(user.MobileNumber))
                {
                    msg = "Please enter Mobile Number";
                }
                else if (user.Amount <= 0)
                {
                    msg = "Please enter Amount";
                }
            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_cleartv)
            {
                if (string.IsNullOrEmpty(user.SubscriberID))
                {
                    msg = "Please enter Subscriber ID";
                }
                else if (string.IsNullOrEmpty(user.MobileNumber))
                {
                    msg = "Please enter Mobile Number";
                }
                else if (user.Amount <= 0)
                {
                    msg = "Please enter Amount";
                }

            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_dishhome)
            {
                if (string.IsNullOrEmpty(user.CasID))
                {
                    msg = "Please enter Cas ID";
                }


            }

            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_jagrititv)
            {
                if (string.IsNullOrEmpty(user.PackageID))
                {
                    msg = "Please enter PackageID";
                }
                else if (string.IsNullOrEmpty(user.PackageName))
                {
                    msg = "Please enter PackageName";
                }
                else if (string.IsNullOrEmpty(user.CasID))
                {
                    msg = "Please enter CasID";
                }
                else if (string.IsNullOrEmpty(user.CustomerID))
                {
                    msg = "Please enter CustomerID";
                }
                else if (string.IsNullOrEmpty(user.CustomerName))
                {
                    msg = "Please enter Customer Name";
                }
                else if (string.IsNullOrEmpty(user.OldWardNumber))
                {
                    msg = "Please enter Old Ward Number";
                }
                else if (string.IsNullOrEmpty(user.MobileNumber))
                {
                    msg = "Please enter Mobile Number";
                }

            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_maxtv)
            {
                if (string.IsNullOrEmpty(user.CustomerID))
                {
                    msg = "Please enter CustomerID";
                }

            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_mero)
            {
                if (string.IsNullOrEmpty(user.STB))
                {
                    msg = "Please enter STB";
                }


            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_prabhutv)
            {
                if (string.IsNullOrEmpty(user.CasID))
                {
                    msg = "Please enter CasID";
                }

            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_tv_simtv)
            {
                if (string.IsNullOrEmpty(user.CustomerID))
                {
                    msg = "Please enter CustomerID";
                }


            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_adsl)
            {
                if (string.IsNullOrEmpty(user.LandlineNumber))
                {
                    msg = "Please enter Landline Number";
                }


            }
            else if ((user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_Arrownet) || (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_classictech))
            {
                if (string.IsNullOrEmpty(user.UserName))
                {
                    msg = "Please enter UserName";
                }


            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_png_network)
            {
                if (string.IsNullOrEmpty(user.PackageID))
                {
                    msg = "Please enter PackageID";
                }
                else if (string.IsNullOrEmpty(user.PackageName))
                {
                    msg = "Please enter Package Name";
                }
                else if (string.IsNullOrEmpty(user.UserName))
                {
                    msg = "Please enter User Name";
                }
                else if (string.IsNullOrEmpty(user.MobileNumber))
                {
                    msg = "Please enter Mobile Number";
                }
                else if (string.IsNullOrEmpty(user.FullName))
                {
                    msg = "Please enter Full Name";
                }


            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_Pokhara)
            {
                if (string.IsNullOrEmpty(user.UserName))
                {
                    msg = "Please enter User Name";
                }
                else if (string.IsNullOrEmpty(user.MobileNumber))
                {
                    msg = "Please enter Mobile Number";
                }
                else if (string.IsNullOrEmpty(user.Address))
                {
                    msg = "Please enter Address";
                }


            }
            else if ((user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_RoyalNetwork) || (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_VirtualNetwork) || (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_WebNetwork))
            {
                if (string.IsNullOrEmpty(user.UserName))
                {
                    msg = "Please enter User Name";
                }


            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_subisu)
            {
                if (string.IsNullOrEmpty(user.UserName))
                {
                    msg = "Please enter User Name";
                }
                else if (string.IsNullOrEmpty(user.MobileNumber))
                {
                    msg = "Please enter Mobile Number";
                }


            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_subisu_new)
            {
                if (string.IsNullOrEmpty(user.UserName))
                {
                    msg = "Please enter User Name";
                }
                


            }
            else if ((user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_TechMinds) || (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_vianet) || (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_WebSurfer) || (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_internet_Worldlink))
            {
                if (string.IsNullOrEmpty(user.CustomerID))
                {
                    msg = "Please enter CustomerID";
                }

            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_pstn_landline)
            {
                if (string.IsNullOrEmpty(user.LandlineNumber))
                {
                    msg = "Please enter Landline Number";
                }



            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_nea)
            {
                if (string.IsNullOrEmpty(user.CounterID))
                {
                    msg = "Please enter Counter ID";
                }
                else if (string.IsNullOrEmpty(user.CounterName))
                {
                    msg = "Please enter Counter Name";
                }
                else if (string.IsNullOrEmpty(user.ScNumber))
                {
                    msg = "Please enter Sc Number";
                }
                else if (string.IsNullOrEmpty(user.ConsumerID))
                {
                    msg = "Please enter Consumer ID";
                }
            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_khanepani)
            {
                if (string.IsNullOrEmpty(user.CounterID))
                {
                    msg = "Please enter Counter ID";
                }
                else if (string.IsNullOrEmpty(user.CounterName))
                {
                    msg = "Please enter Counter Name";
                }
                else if (string.IsNullOrEmpty(user.CustomerID))
                {
                    msg = "Please enter CustomerID";
                }
            }
            else if ((user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Antivirus_Eset) || (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Antivirus_k7) || (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_antivirus_kaspersky) || (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Antivirus_Mcafee) || (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Antivirus_Wardwiz))
            {
                if (string.IsNullOrEmpty(user.SubscriptionID))
                {
                    msg = "Please enter Subscription ID";
                }
                else if (string.IsNullOrEmpty(user.SubscriptionName))
                {
                    msg = "Please enter Subscription Name";
                }

            }
            else if ((user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_ncell) || (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_ntc))
            {
                if (string.IsNullOrEmpty(user.MobileNumber))
                {
                    msg = "Please enter Mobile Number";
                }

            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Arhant)
            {
                if (string.IsNullOrEmpty(user.AcceptanceNo))
                {
                    msg = "Please enter Acceptance No";
                }
            }
            else if ((user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Surya_Jyoti_Life) || (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Nepal_Life) || (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Sanima_Reliance))
            {
                if (string.IsNullOrEmpty(user.PolicyNumber))
                {
                    msg = "Please enter Policy Number";
                }
                else if (string.IsNullOrEmpty(user.DateOfBirth))
                {
                    msg = "Please enter Date Of Birth";
                }
            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Neco)
            {
                if (string.IsNullOrEmpty(user.InsuranceID))
                {
                    msg = "Please enter Insurance ID";
                }
                else if (string.IsNullOrEmpty(user.InsuranceName))
                {
                    msg = "Please enter Insurance Name";
                }
            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Salico)
            {
                if (string.IsNullOrEmpty(user.DebitNoteNo))
                {
                    msg = "Please enter Debit Note No";
                }
            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Insurance_Shikhar)
            {
                if (string.IsNullOrEmpty(user.CustomerName))
                {
                    msg = "Please enter Customer Name";
                }
                else if (string.IsNullOrEmpty(user.MobileNumber))
                {
                    msg = "Please enter Mobile Number";
                }
                else if (string.IsNullOrEmpty(user.Email))
                {
                    msg = "Please enter Email";
                }
                else if (string.IsNullOrEmpty(user.PolicyType))
                {
                    msg = "Please enter Policy Type";
                }
                else if (string.IsNullOrEmpty(user.Branch))
                {
                    msg = "Please enter Branch";
                }
                else if (string.IsNullOrEmpty(user.PolicyNumber))
                {
                    msg = "Please enter Policy Number";
                }
                else if (string.IsNullOrEmpty(user.PolicyCategory))
                {
                    msg = "Please enter Policy Category";
                }
                else if (string.IsNullOrEmpty(user.Address))
                {
                    msg = "Please enter Address";
                }
                else if (string.IsNullOrEmpty(user.PolicyDescription))
                {
                    msg = "Please enter Policy Description";
                }

            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.khalti_Traffic_Police_Fine)
            {
                if (string.IsNullOrEmpty(user.ChitNumber))
                {
                    msg = "Please enter Chit Number";
                }
                else if (string.IsNullOrEmpty(user.FiscalYearID))
                {
                    msg = "Please enter Fiscal Year ID";
                }
                else if (string.IsNullOrEmpty(user.FiscalYearValue))
                {
                    msg = "Please enter Fiscal Year";
                }
                else if (string.IsNullOrEmpty(user.ProvinceID))
                {
                    msg = "Please enter Province ID";
                }
                else if (string.IsNullOrEmpty(user.ProvinceName))
                {
                    msg = "Please enter ProvinceName";
                }
                else if (string.IsNullOrEmpty(user.DistrictID))
                {
                    msg = "Please enter District ID";
                }
                else if (string.IsNullOrEmpty(user.DistrictValue))
                {
                    msg = "Please enter District Value";
                }
            }
            else if (user.ServiceID == (int)VendorApi_CommonHelper.KhaltiAPIName.Credit_Card_Payment)
            {
                if (string.IsNullOrEmpty(user.BankID))
                {
                    msg = "Please enter Bank ID";
                }
                else if (string.IsNullOrEmpty(user.BankName))
                {
                    msg = "Please enter Bank Name";
                }
                else if (string.IsNullOrEmpty(user.CreditCardNumber))
                {
                    msg = "Please enter Credit Card Number";
                }
                else if (string.IsNullOrEmpty(user.CreditCardOwner))
                {
                    msg = "Please enter Credit Card Owner";
                }
            }
            else if (user.Amount <= 0)
            {
                msg = "Please enter Amount";
            }
            return msg;
        }
    }
}