using MyPay.Models.Common;
using MyPay.Models.NepalPayQR;
using MyPay.Models.VendorAPI.Get.BusSewaService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static MyPay.Models.OrganizationModel;

namespace MyPay.API.Models
{
    public class CommonApiMethod
    {
        public static CommonResponse ReturnBadRequestMessage(string results)
        {
            CommonResponse result = new CommonResponse();
            result.Message = results;
            result.Details = results;
            result.responseMessage = results;
            if (results == Common.Invalidusertoken)
            {
                result.ReponseCode = 6;
            }
            else if (results == Common.SessionExpired)
            {
                result.ReponseCode = 7;
            }
            else if (results == Common.InactiveUserMessage)
            {
                result.ReponseCode = 11;
            }
            else if (results == Common.Relogin)
            {
                result.ReponseCode = 11;
            }
            else if (results == Common.LoginWithOTP)
            {
                result.ReponseCode = 12;
            }
            else if (results == Common.Invalidpin)
            {
                result.ReponseCode = 13;
            }
            else if (results == "Can't Fulfill Request You Have No Pending Bills")
            {
                result.ReponseCode = 3;
            }
            else if ((results.ToLower().Contains("pending")) || (results.ToLower().Contains("queued")) || (results.ToLower().Contains("processing")))
            {
                result.ReponseCode = 14;
            }
            else
            {
                result.ReponseCode = 3;
            }

            result.status = false;
            return result;

        }
        public static CommonResponseDataOrganization ReturnBadRequestMessages(string results)
        {
            CommonResponse result = new CommonResponse();
            result.Message = results;
            result.Details = results;
            result.responseMessage = results;
            if (results == Common.Invalidusertoken)
            {
                result.ReponseCode = 6;
            }
            else if (results == Common.SessionExpired)
            {
                result.ReponseCode = 7;
            }
            else if (results == Common.InactiveUserMessage)
            {
                result.ReponseCode = 11;
            }
            else if (results == Common.Relogin)
            {
                result.ReponseCode = 11;
            }
            else if (results == Common.LoginWithOTP)
            {
                result.ReponseCode = 12;
            }
            else if (results == Common.Invalidpin)
            {
                result.ReponseCode = 13;
            }
            else if ((results.ToLower().Contains("pending")) || (results.ToLower().Contains("queued")) || (results.ToLower().Contains("processing")))
            {
                result.ReponseCode = 14;
            }
            else
            {
                result.ReponseCode = 3;
            }

            result.status = false;
            return null;

        }
        public static CommonDBResponse ReturnBad_RequestMessage(string results)
        {
            CommonDBResponse result = new CommonDBResponse();
            result.Message = results;
            result.Details = results;
            result.responseMessage = results;
            if (results == Common.Invalidusertoken)
            {
                result.ReponseCode = 6;
            }
            else if (results == Common.SessionExpired)
            {
                result.ReponseCode = 7;
            }
            else if (results == Common.InactiveUserMessage)
            {
                result.ReponseCode = 11;
            }
            else if (results == Common.Relogin)
            {
                result.ReponseCode = 11;
            }
            else if (results == Common.LoginWithOTP)
            {
                result.ReponseCode = 12;
            }
            else if (results == Common.Invalidpin)
            {
                result.ReponseCode = 13;
            }
            else if ((results.ToLower().Contains("pending")) || (results.ToLower().Contains("queued")) || (results.ToLower().Contains("processing")))
            {
                result.ReponseCode = 14;
            }
            else
            {
                result.ReponseCode = 3;
            }

            result.status = false;
            return result;

        }
        public static commonresponsedata BusSewaReturnBadRequestMessage(string results)
        {
            commonresponsedata result = new commonresponsedata();
            result.Message = results;
            if (results == Common.Invalidusertoken)
            {
                result.ReponseCode = 6;
            }
            else if (results == Common.SessionExpired)
            {
                result.ReponseCode = 7;
            }
            else if (results == Common.InactiveUserMessage)
            {
                result.ReponseCode = 11;
            }
            else if (results == Common.Relogin)
            {
                result.ReponseCode = 11;
            }
            else if (results == Common.LoginWithOTP)
            {
                result.ReponseCode = 12;
            }
            else if (results == Common.Invalidpin)
            {
                result.ReponseCode = 13;
            }
            else if ((results.ToLower().Contains("pending")) || (results.ToLower().Contains("queued")) || (results.ToLower().Contains("processing")))
            {
                result.ReponseCode = 14;
            }
            else
            {
                result.ReponseCode = 3;
            }

            result.status = false;
            return result;

        }
        public static CommonResponseData AllReturnBadRequestMessage(string results)
        {
            CommonResponseData result = new CommonResponseData();
            result.Message = results;
            if (results == Common.Invalidusertoken)
            {
                result.ReponseCode = 6;
            }
            else if (results == Common.SessionExpired)
            {
                result.ReponseCode = 7;
            }
            else if (results == Common.InactiveUserMessage)
            {
                result.ReponseCode = 11;
            }
            else if (results == Common.Relogin)
            {
                result.ReponseCode = 11;
            }
            else if (results == Common.LoginWithOTP)
            {
                result.ReponseCode = 12;
            }
            else if (results == Common.Invalidpin)
            {
                result.ReponseCode = 13;
            }
            else if ((results.ToLower().Contains("pending")) || (results.ToLower().Contains("queued")) || (results.ToLower().Contains("processing")))
            {
                result.ReponseCode = 14;
            }
            else
            {
                result.ReponseCode = 3;
            }

            result.status = false;
            return result;

        }

       
    }
}