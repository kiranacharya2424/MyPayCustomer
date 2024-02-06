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
    public class BaseMerchantSessionController : Controller
    {

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            MyPayEntities context = new MyPayEntities();
            try
            {
                var isAjax = Request.IsAjaxRequest();
                if (!isAjax)
                {
                    if (Session["MerchantRole"] != null && Session["MerchantRole"].ToString() != "0" && Session["MerchantRole"].ToString() != "1")
                    {
                        string currenturl = Request.Url.AbsolutePath;
                        var RoleId = int.Parse(Session["MerchantRole"].ToString());
                        var menus = (from m in context.Menus
                                     join ma in context.MenuAssigns on m.Id equals ma.MenuId
                                     where (m.IsActive == true && m.IsDeleted == false && ma.RoleId == RoleId && m.Url.ToLower() == (currenturl.ToLower()))
                                     select new
                                     {
                                         m.Id,
                                         m.Icon,
                                         m.IsActive,
                                         m.IsDeleted,
                                         m.MenuName,
                                         m.ParentId,
                                         m.Url,
                                         ma.MenuId,
                                         ma.RoleId
                                     }).FirstOrDefault();
                        if (menus == null)
                        {
                            //Response.Redirect("/MerchantLogin");
                        }
                    }
                    else if (Session["MerchantRole"] == null || Session["MerchantRole"].ToString() == "0" || Session["MerchantUniqueId"] == null)
                    {
                        Response.Redirect("/MerchantLogin");
                    }
                    AddMerchant outobject = new AddMerchant();
                    GetMerchant inobject = new GetMerchant();
                    inobject.MerchantUniqueId = Convert.ToString(Session["MerchantUniqueId"]);
                    AddMerchant model = RepCRUD<GetMerchant, AddMerchant>.GetRecord(Common.StoreProcedures.sp_Merchant_Get, inobject, outobject);
                    if (model != null && model.Id != 0)
                    {
                        if (model.IsPasswordReset == false)
                        {
                            Response.Redirect("/MerchantLogin/ChangePassword");
                        }
                    }
                }
                // AddBalanceHistory.DisconnectedServiceConnection();
                 //AddBalanceHistoryMerchant.DisconnectedServiceConnection();
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