using log4net;
using MyPay.API.Models;
using MyPay.API.Models.Request;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using MyPay.Repository;
using Newtonsoft.Json;
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

namespace MyPay.API.Controllers
{
    public class CheckFonePayResultController : ApiController
    {

        public HttpResponseMessage Post(Req_AddTicket user)
        {

            CommonResponse result = new CommonResponse();
            var response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, result);
            try
            {
                Req_FonePayToken req = new Req_FonePayToken();
                req.grant_type = "password";
                req.username = VendorApi_CommonHelper.FonePay_UserName;
                req.password = VendorApi_CommonHelper.FonePay_Password;
                string result1 = RepFonePay.GenerateFonePayAuthToken();
               //string result2 = RepFonePay.PostFundMethod(JsonConvert.SerializeObject(req));
                CommonResponse cres1 = new CommonResponse();
                //cres1 = CommonApiMethod.ReturnBadRequestMessage(result1+"@@@"+result2);
                cres1 = CommonApiMethod.ReturnBadRequestMessage(result1);
                cres1.Details = Common.GetUserIP() + ":" + Common.GetServerIPAddress();
                response.StatusCode = HttpStatusCode.BadRequest;
               
                response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres1);
                return response;


            }
            catch (Exception ex)
            {

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