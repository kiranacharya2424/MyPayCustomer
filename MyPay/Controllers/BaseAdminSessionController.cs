using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Repository;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyPay.Controllers
{
    public class BaseAdminSessionController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            MyPayEntities context = new MyPayEntities();
            try
            {
                if (Session["AdminMemberId"] == null)
                {
                    string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
                    HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
                    if (authCookie != null)
                    {
                        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
                        string UserName = ticket.Name; //You have the UserName!

                        AddAdmin outobject = new AddAdmin();
                        GetAdmin inobject = new GetAdmin();
                        inobject.UserName = UserName;
                        inobject.CheckDelete = 0;
                        inobject.CheckActive = 1;
                        inobject.RoleId = 1;
                        AddAdmin res = RepCRUD<GetAdmin, AddAdmin>.GetRecord(Common.StoreProcedures.sp_AdminUser_Get, inobject, outobject);

                        if (res != null && res.Id != 0)
                        {
                            Common.SetAdminSession(res);
                        }
                    }
                }
                var isAjax = Request.IsAjaxRequest();
                if (!isAjax) 
                {
                    (new CommonHelpers()).AccessRolesAuthentication();
                }
                AddBalanceHistory.DisconnectedServiceConnection();
                if (!Common.ApplicationEnvironment.IsProduction)
                {
                    Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.Req_TokenTest = Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.Req_TokenTest_localhost;
                    Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.Req_TokenLive = Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.Req_TokenLive_localhost;
                    Models.Common.Common.SendSMSToken = Models.Common.Common.LocalSendSMSToken;
                }
                else
                {
                    Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.Req_TokenTest = "yKutgSRojJTpcnxKBPOx";
                    Models.VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.Req_TokenLive = "yKutgSRojJTpcnxKBPOx";
                    Models.Common.Common.SendSMSToken = "28826134c0c12588ebf248d27926ec849476c3f98ceab1536f12326c84421772";
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}