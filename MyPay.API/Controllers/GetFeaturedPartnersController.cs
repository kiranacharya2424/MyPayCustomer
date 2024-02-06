using log4net;
using MyPay.API.Models;
using MyPay.API.Models.MyPayPayments;
using MyPay.API.Models.Request.Merchant;
using MyPay.API.Models.Response.Merchant;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;
using static System.Net.Mime.MediaTypeNames;

namespace MyPay.API.Controllers
{
    public class GetFeaturedPartnersController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(GetFeaturedPartnersController));

        string ApiResponse = string.Empty;
        
        [HttpPost]
        [Route("api/getfeaturedPartners")]
        public HttpResponseMessage GetFeature(Req_FeaturedPartners user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside features" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            List<GetFeaturedPartners> list = new List<GetFeaturedPartners>();
            
            Res_FeaturedPartners result = new Res_FeaturedPartners();
            var response = Request.CreateResponse<Res_FeaturedPartners>(System.Net.HttpStatusCode.BadRequest, result);

            var userInput = getRawPostData().Result;

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
                    string md5hash = Common.getHashMD5(userInput); //Common.CheckHash(user);

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

                        GetFeaturedPartners obj = new GetFeaturedPartners();
                        obj.OrganizationName = user.OrganizationName;
                        obj.Image = user.Image;
                        obj.WebsiteUrl = user.WebsiteUrl;
                        obj.SortOrder = user.SortOrder;
                        obj.IsFeaturedPartner = user.IsFeaturedPartner;
                        var dir = Common.LiveSiteUrl + "Images/MerchantImages/";
                        DataTable result1 = obj.Get();
                        List<GetFeaturedPartners> dataList = new List<GetFeaturedPartners>(); // Create a list to store the results

                        if (result1.Rows.Count > 0)
                        {
                            foreach (DataRow row in result1.Rows)
                            {
                                GetFeaturedPartners obj1 = new GetFeaturedPartners
                                {
                                    OrganizationName = row["OrganizationName"].ToString(),
                                    Image = dir + row["Image"].ToString(),
                                    WebsiteUrl = row["WebsiteUrl"].ToString(),
                                    SortOrder = Convert.ToInt32(row["SortOrder"].ToString()),
                                    IsFeaturedPartner = row["IsFeaturedPartner"].Equals("True")
                                };

                                dataList.Add(obj1); // Add the object to the list
                            }
                            cres.Data = dataList;
                            cres.Message = "success";
                            cres.status = true;
                            cres.ReponseCode = 1;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.OK, cres);
                            log.Info($"{System.DateTime.Now.ToString()} use-fetaured partner completed" + Environment.NewLine);

                            return response;
                        }

                        return response;


                    }

                }

                log.Info($"{System.DateTime.Now.ToString()} Featured Partner completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} use-features {ex.ToString()} " + Environment.NewLine);
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