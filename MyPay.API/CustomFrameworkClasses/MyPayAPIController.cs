using MyPay.API.Models;
using MyPay.API.Models.Events;
using MyPay.API.Models.Request.Voting.Consumer;
using MyPay.Models.Add;
using MyPay.Models.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace MyPay.API.CustomFrameworkClasses
{
    public class MyPayAPIController : ApiController
    {
        public HttpResponseMessage checkIfAuthorizationExists(HttpRequestMessage req)
        {
            CommonResponse cres = new CommonResponse();
            var response = req.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.OK, cres);
            if (req.Headers.Authorization == null)
            {
                string results = "Un-Authorized Request";
                cres = CommonApiMethod.ReturnBadRequestMessage(results);
                response.StatusCode = HttpStatusCode.BadRequest;
                response = req.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
                return response;
            }
            return response;
        }

        internal HttpResponseMessage createErrorRespWithMessage(HttpRequestMessage req, string message)
        {
            CommonResponse cres = new CommonResponse();
            var response = req.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.OK, cres);

            cres = CommonApiMethod.ReturnBadRequestMessage(message);
            response.StatusCode = HttpStatusCode.BadRequest;
            response = Request.CreateResponse<CommonResponse>(System.Net.HttpStatusCode.BadRequest, cres);
            return response;
        }

     

        internal async Task<String> getRawPostData()
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

        internal HttpResponseMessage performGenericValidation(ICommonVotingProps req, string UserInput, bool checkUserDetail = false)
        {
            var cres = new CommonResponse();
            var response = Request.CreateResponse<CommonResponse>(HttpStatusCode.OK, cres);

            var authResponse = checkIfAuthorizationExists(Request);
            if (authResponse.StatusCode != HttpStatusCode.OK)
            {
                return authResponse;
            }

            var requestJSON = JsonConvert.SerializeObject(req);

            string md5hash = Common.getHashMD5(UserInput); 
           // Common.CheckHash(req);
            string CommonPropsCheckResult = new CommonHelpers().CheckApiToken(req.Hash, req.TimeStamp, md5hash, req.PlatForm, req.Version, req.DeviceCode, req.SecretKey, UserInput);
            if (CommonPropsCheckResult != "Success")
            {
                var CommonPropsCheckResponse = createErrorRespWithMessage(Request, CommonPropsCheckResult);
                return CommonPropsCheckResponse;
            }

            if (!string.IsNullOrEmpty(req.MemberId) && req.MemberId != "0")
            {
                // string UserInput = getRawPostData().Result;
                string CommonResult = "";
                AddUserLoginWithPin resuserdetails = new AddUserLoginWithPin();
                AddCouponsScratched resGetCouponsScratched = new AddCouponsScratched();
                Int64 memId = Convert.ToInt64(req.MemberId);
                int VendorAPIType = (int)MyPay.Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName.Voting;
                int Type = 0;
                if (checkUserDetail)
                {
                    resuserdetails = new CommonHelpers().CheckUserDetail(req.UniqueCustomerId, UserInput, "", "", ref resGetCouponsScratched, "", req.DeviceId, ref CommonResult, Type, VendorAPIType, memId, false, false, "0", false, req.Mpin, "", false, true);
                    if (CommonResult.ToLower() != "success")
                    {
                        var CommonPropsCheckResponse = createErrorRespWithMessage(Request, CommonResult);
                        return CommonPropsCheckResponse;
                    }
                }
            }
            return response;
        }

        internal string getHashMD5(string JSONInput)
        {

            //var jsonObject = JsonConvert.DeserializeObject(JSONInput);
            string concatenated = "";

            JObject jsonObject = JObject.Parse(JSONInput);

            foreach (var item in jsonObject)
            {
                if (item.Value != null && !string.IsNullOrEmpty(item.Value.ToString()) && item.Key.ToLower() != "hash" &&
                        item.Key.ToLower() != "passengersclassstring" && item.Key.ToLower() != "playerlist" && item.Key.ToLower() != "bankdata" &&
                        item.Key.ToLower() != "vendorjsonlookup")
                {
                    if (item.Key.ToLower() == "mpin")
                    {
                        concatenated += Common.Decryption(item.Value.ToString());
                    }
                    else
                    {
                        concatenated += item.Value.ToString();
                    }
                }

            }

            string result = "";
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(concatenated));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();

        }
    }
}
