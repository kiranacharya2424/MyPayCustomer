using log4net;
using MyPay.API.Models;
using MyPay.API.Models.Request;
using MyPay.API.Models.Response;
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
using System.Web.Http;

namespace MyPay.API.Controllers
{
    public class GetOfferBannersController : ApiController
    {
        private static ILog log = LogManager.GetLogger(typeof(GetOfferBannersController));
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/getofferbanners")]
        public HttpResponseMessage GetOfferBanners(Req_OfferBanners user)
        {
            log.Info($"{System.DateTime.Now.ToString()} inside GetOfferBanners" + Environment.NewLine);
            CommonResponse cres = new CommonResponse();
            List<AddOfferBanners> list = new List<AddOfferBanners>();
            List<ProviderServiceCategoryList> plist = new List<ProviderServiceCategoryList>();
            List<AddMarque> listmarque = new List<AddMarque>();
            Res_OfferBanners result = new Res_OfferBanners();
            var response = Request.CreateResponse<Res_OfferBanners>(System.Net.HttpStatusCode.BadRequest, result);

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
                        res = new CommonHelpers().CheckUserDetail(user.UniqueCustomerId, "", "", user.BankTransactionId, ref resGetCouponsScratched, user.CouponCode, user.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, user.Mpin, "", true);
                        if (CommonResult.ToLower() != "success")
                        {
                            CommonResponse cres1 = new CommonResponse();
                            cres1 = CommonApiMethod.ReturnBadRequestMessage(CommonResult);
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                            return response;
                        }
                        if (user.IsHome == "0")
                        {
                            MyPayEntities context = new MyPayEntities();
                            plist = context.ProviderServiceCategoryLists.ToList();

                            AddMarque outmobject = new AddMarque();
                            GetMarque inmobject = new GetMarque();
                            inmobject.CheckDelete = 0;
                            inmobject.CheckActive = 1;
                            listmarque = RepCRUD<GetMarque, AddMarque>.GetRecordList("sp_Marque_Get", inmobject, outmobject);
                        }

                        AddOfferBanners outobject = new AddOfferBanners();
                        GetOfferBanners inobject = new GetOfferBanners();
                        inobject.CheckActive = 1;
                        inobject.Running = "1";
                        inobject.IsHome = Convert.ToInt32(user.IsHome);
                        list = RepCRUD<GetOfferBanners, AddOfferBanners>.GetRecordList(Common.StoreProcedures.sp_OfferBanners_Get, inobject, outobject);
                        for (int i = 0; i < list.Count; i++)
                        {
                            list[i].Image = Common.LiveSiteUrl + $"/Images/BannerImages/" + list[i].Image;
                            list[i].BannerImage = Common.LiveSiteUrl + $"/Images/BannerImages/" + list[i].BannerImage;
                        }
                        List<AddReferEarnImage> ReferEarnlist = new List<AddReferEarnImage>();
                        AddReferEarnImage outobjectReferEarn = new AddReferEarnImage();
                        GetReferEarnImage inobjectReferEarn = new GetReferEarnImage();
                        inobjectReferEarn.CheckActive = 1;
                        ReferEarnlist = RepCRUD<GetReferEarnImage, AddReferEarnImage>.GetRecordList(Common.StoreProcedures.sp_ReferEarnImage_Get, inobjectReferEarn, outobjectReferEarn);
                        for (int i = 0; i < ReferEarnlist.Count; i++)
                        {
                            ReferEarnlist[i].Image = Common.LiveSiteUrl + $"/Images/ReferEarnImages/" + ReferEarnlist[i].Image;
                        }
                        result.ReferEarndata = ReferEarnlist;
                        result.status = true;
                        result.data = list;
                        result.ProviderList = plist;
                        result.MarqueList = listmarque;
                        result.ReponseCode = 1;
                        result.Message = "success";
                        response.StatusCode = HttpStatusCode.Accepted;
                        response = Request.CreateResponse<Res_OfferBanners>(System.Net.HttpStatusCode.OK, result);
                    }
                }
                log.Info($"{System.DateTime.Now.ToString()} GetOfferBanners completed" + Environment.NewLine);
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
                log.Error($"{System.DateTime.Now.ToString()} GetOfferBanners {ex.ToString()} " + Environment.NewLine);
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