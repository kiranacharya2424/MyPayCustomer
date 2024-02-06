using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPay.Controllers
{
    public class BaseUserAuthorizationSessionController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        { 
            base.OnActionExecuting(filterContext);
            if (Common.ApplicationEnvironment.IsProduction)
            {
                RepNCHL.NCHLApiUrl_LinkBank = RepNCHL.NCHLApiUrl;
                RepNCHL.NCHLWithdrawAPIURl_LinkBank = RepNCHL.NCHLWithdrawAPIURl;
                RepNCHL.APPID_LinkBank = RepNCHL.APPID;
                RepNCHL.APPID_NCHL_STAGING = RepNCHL.APPID_STAGE_PAYMENT_LIVE;
                RepNCHL.UserId_LinkBank = RepNCHL.UserId;
                RepNCHL.APPNAME_LinkBank = RepNCHL.APPNAME;
                RepNCHL.WithdrawBasicApiKey_LinkBank = RepNCHL.WithdrawBasicApiKey;
                RepNCHL.withdrawusername_LinkBank = RepNCHL.withdrawusername;
                RepNCHL.withdrawpassword_LinkBank = RepNCHL.withdrawpassword;
                RepNCHL.participantId_LinkBank = "MYPAY";
                RepNCHL.PFXFile_LinkBank = RepNCHL.PFXFile;
                RepNCHL.PFXPassword_LinkBank = RepNCHL.PFXPassword;
                RepNCHL.TokenizationURL_LinkBank = RepNCHL.TokenizationURL;
                RepNCHL.TokenizationLinkBankAPIURl_LinkBank = RepNCHL.TokenizationLinkBankAPIURl;
            }

        }

    }
}