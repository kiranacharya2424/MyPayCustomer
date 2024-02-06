using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Miscellaneous;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyPay.Repository
{
    public static class RepCRUD<TParent, TChild> where TParent : class where TChild : class
    {

        public static Int64 Insert(TParent myobject, string TableName)
        {
            Int64 _ret = 0;
            using (var context = new MyPayEntities())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    _ret = RepoInsert(myobject, TableName, context, dbContextTransaction);
                }
            }
            return _ret;
        }
        public static bool Update(TParent myobject, string TableName)
        {
            var _ret = false;
            using (var context = new MyPayEntities())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    _ret = RepoUpdate(myobject, TableName, context, dbContextTransaction);
                }
            }
            return _ret;
        }
        public static TChild GetRecord(string TableName, TParent inobject, TChild outobject)
        {

            using (var context = new MyPayEntities())
            {
                outobject = RepoRecord(TableName, context, inobject, outobject);
            }
            return outobject;

        }
        public static List<TChild> GetRecordList(string TableName, TParent inobject, TChild outobject)
        {

            using (var context = new MyPayEntities())
            {
                List<TChild> outobject_return = RepoRecordList(TableName, context, inobject, outobject);
                return outobject_return;
            }

        }

        public static Int64 InsertList(List<TParent> myobject, string TableName)
        {
            Int64 _ret = 0;
            using (var context = new MyPayEntities())
            {
                _ret = RepoInsertList(myobject, TableName, context);
            }
            return _ret;
        }
        private static bool RepoUpdate(TParent myobject, string TableName, MyPayEntities context, DbContextTransaction dbcontext)
        {
            var objret = false;
            try
            {
                switch (TableName.ToLower())
                {
                    case "menus":
                        Menu objMenu = new Menu();
                        PropertyCopier<TParent, Menu>.Copy(myobject, objMenu);
                        var dataobjMenu = context.Menus.SingleOrDefault(b => b.Id == objMenu.Id);
                        // Checking if any such record exist 
                        if (dataobjMenu != null)
                        {
                            PropertyCopier<TParent, Menu>.Copy(myobject, dataobjMenu);
                            dataobjMenu.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;

                      case "balancehistorymerchant":
                        BalanceHistoryMerchant objbalhisM = new BalanceHistoryMerchant();
                        PropertyCopier<TParent, BalanceHistoryMerchant>.Copy(myobject, objbalhisM);
                        var dataobjbalhisM = context.BalanceHistoryMerchants.SingleOrDefault(a => a.Id == objbalhisM.Id);
                        // Checking if any such record exist 
                        if (dataobjbalhisM != null)
                        {
                            PropertyCopier<TParent, BalanceHistoryMerchant>.Copy(myobject, dataobjbalhisM);
                            dataobjbalhisM.UpdatedDate = System.DateTime.UtcNow;
                   
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;

                    case "couponsscratched":
                        CouponsScratched objCouponsScratched = new CouponsScratched();
                        PropertyCopier<TParent, CouponsScratched>.Copy(myobject, objCouponsScratched);
                        var dataobjCouponsScratched = context.CouponsScratcheds.SingleOrDefault(b => b.Id == objCouponsScratched.Id);
                        // Checking if any such record exist 
                        if (dataobjCouponsScratched != null)
                        {
                            PropertyCopier<TParent, CouponsScratched>.Copy(myobject, dataobjCouponsScratched);
                            dataobjCouponsScratched.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "coupons":
                        Coupon coupon = new Coupon();
                        PropertyCopier<TParent, Coupon>.Copy(myobject, coupon);
                        var datacoupon = context.Coupons.SingleOrDefault(b => b.Id == coupon.Id);
                        // Checking if any such record exist 
                        if (datacoupon != null)
                        {
                            PropertyCopier<TParent, Coupon>.Copy(myobject, datacoupon);
                            datacoupon.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "merchantwithdrawalrequest":
                        MerchantWithdrawalRequest objMerchantWithdrawalRequest = new MerchantWithdrawalRequest();
                        PropertyCopier<TParent, MerchantWithdrawalRequest>.Copy(myobject, objMerchantWithdrawalRequest);
                        var dataobjMerchantWithdrawalRequest = context.MerchantWithdrawalRequests.SingleOrDefault(b => b.Id == objMerchantWithdrawalRequest.Id);
                        // Checking if any such record exist 
                        if (dataobjMerchantWithdrawalRequest != null)
                        {
                            PropertyCopier<TParent, MerchantWithdrawalRequest>.Copy(myobject, dataobjMerchantWithdrawalRequest);
                            dataobjMerchantWithdrawalRequest.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "apisettingshistory":
                        ApiSettingsHistory objapisettingshistory = new ApiSettingsHistory();
                        PropertyCopier<TParent, ApiSettingsHistory>.Copy(myobject, objapisettingshistory);
                        var dataobjapisettingshistory = context.ApiSettingsHistories.SingleOrDefault(b => b.Id == objapisettingshistory.Id);
                        // Checking if any such record exist 
                        if (dataobjapisettingshistory != null)
                        {
                            PropertyCopier<TParent, ApiSettingsHistory>.Copy(myobject, dataobjapisettingshistory);
                            dataobjapisettingshistory.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "merchantbankdetail":
                        MerchantBankDetail objMerchantbank = new MerchantBankDetail();
                        PropertyCopier<TParent, MerchantBankDetail>.Copy(myobject, objMerchantbank);
                        var dataobjMerchantbank = context.MerchantBankDetails.SingleOrDefault(b => b.Id == objMerchantbank.Id);
                        // Checking if any such record exist 
                        if (dataobjMerchantbank != null)
                        {
                            PropertyCopier<TParent, MerchantBankDetail>.Copy(myobject, dataobjMerchantbank);
                            dataobjMerchantbank.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "merchant":
                        Merchant objMerchant = new Merchant();
                        PropertyCopier<TParent, Merchant>.Copy(myobject, objMerchant);
                        var dataobjMerchant = context.Merchants.SingleOrDefault(b => b.Id == objMerchant.Id);
                        // Checking if any such record exist 
                        if (dataobjMerchant != null)
                        {
                            PropertyCopier<TParent, Merchant>.Copy(myobject, dataobjMerchant);
                            dataobjMerchant.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "merchantipaddress":
                        MerchantIPAddress objMerchantIP = new MerchantIPAddress();
                        PropertyCopier<TParent, MerchantIPAddress>.Copy(myobject, objMerchantIP);
                        var dataobjMerchantIp = context.MerchantIPAddresses.SingleOrDefault(b => b.Id == objMerchantIP.Id);
                        // Checking if any such record exist 
                        if (dataobjMerchantIp != null)
                        {
                            PropertyCopier<TParent, MerchantIPAddress>.Copy(myobject, dataobjMerchantIp);
                            dataobjMerchantIp.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "datapackdetail":
                        DataPackDetail objdatapack = new DataPackDetail();
                        PropertyCopier<TParent, DataPackDetail>.Copy(myobject, objdatapack);
                        var dataobjdatapack = context.DataPackDetails.SingleOrDefault(b => b.Id == objdatapack.Id);
                        // Checking if any such record exist 
                        if (dataobjdatapack != null)
                        {
                            PropertyCopier<TParent, DataPackDetail>.Copy(myobject, dataobjdatapack);
                            dataobjdatapack.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "userinactiveremarks":
                        UserInActiveRemark objusr = new UserInActiveRemark();
                        PropertyCopier<TParent, UserInActiveRemark>.Copy(myobject, objusr);
                        var dataobjusr = context.UserInActiveRemarks.SingleOrDefault(b => b.Id == objusr.Id);
                        // Checking if any such record exist 
                        if (dataobjusr != null)
                        {
                            PropertyCopier<TParent, UserInActiveRemark>.Copy(myobject, dataobjusr);
                            dataobjusr.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "notificationcampaign":
                        NotificationCampaign objNotificationCampaign = new NotificationCampaign();
                        PropertyCopier<TParent, NotificationCampaign>.Copy(myobject, objNotificationCampaign);
                        var dataobjNotificationCampaign = context.NotificationCampaigns.SingleOrDefault(b => b.Id == objNotificationCampaign.Id);
                        // Checking if any such record exist 
                        if (dataobjNotificationCampaign != null)
                        {
                            PropertyCopier<TParent, NotificationCampaign>.Copy(myobject, dataobjNotificationCampaign);
                            dataobjNotificationCampaign.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "insurancedetail":
                        InsuranceDetail objInsuranceDetail = new InsuranceDetail();
                        PropertyCopier<TParent, InsuranceDetail>.Copy(myobject, objInsuranceDetail);
                        var dataobjInsuranceDetail = context.InsuranceDetails.SingleOrDefault(b => b.Id == objInsuranceDetail.Id);
                        // Checking if any such record exist 
                        if (dataobjInsuranceDetail != null)
                        {
                            PropertyCopier<TParent, InsuranceDetail>.Copy(myobject, dataobjInsuranceDetail);
                            dataobjInsuranceDetail.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "exportdata":
                        ExportData objExportData = new ExportData();
                        PropertyCopier<TParent, ExportData>.Copy(myobject, objExportData);
                        var dataobjExportData = context.ExportDatas.SingleOrDefault(b => b.Id == objExportData.Id);
                        // Checking if any such record exist 
                        if (dataobjExportData != null)
                        {
                            PropertyCopier<TParent, ExportData>.Copy(myobject, dataobjExportData);
                            dataobjExportData.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "referearnimage":
                        ReferEarnImage objReferEarnImage = new ReferEarnImage();
                        PropertyCopier<TParent, ReferEarnImage>.Copy(myobject, objReferEarnImage);
                        var dataobjReferEarnImage = context.ReferEarnImages.SingleOrDefault(b => b.Id == objReferEarnImage.Id);
                        // Checking if any such record exist 
                        if (dataobjReferEarnImage != null)
                        {
                            PropertyCopier<TParent, ReferEarnImage>.Copy(myobject, dataobjReferEarnImage);
                            dataobjReferEarnImage.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "sharereferlink":
                        ShareReferLink objShareReferLink = new ShareReferLink();
                        PropertyCopier<TParent, ShareReferLink>.Copy(myobject, objShareReferLink);
                        var dataobjShareReferLink = context.ShareReferLinks.SingleOrDefault(b => b.Id == objShareReferLink.Id);
                        if (dataobjShareReferLink != null)
                        {
                            PropertyCopier<TParent, ShareReferLink>.Copy(myobject, dataobjShareReferLink);
                            dataobjShareReferLink.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "roles":
                        Role objRole = new Role();
                        PropertyCopier<TParent, Role>.Copy(myobject, objRole);
                        var dataobjRole = context.Roles.SingleOrDefault(b => b.Id == objRole.Id);
                        // Checking if any such record exist 
                        if (dataobjRole != null)
                        {
                            PropertyCopier<TParent, Role>.Copy(myobject, dataobjRole);
                            dataobjRole.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "menuassign":
                        MenuAssign objMenuAssign = new MenuAssign();
                        PropertyCopier<TParent, MenuAssign>.Copy(myobject, objMenuAssign);
                        var dataobjMenuAssign = context.MenuAssigns.SingleOrDefault(b => b.Id == objMenuAssign.Id);
                        // Checking if any such record exist 
                        if (dataobjMenuAssign != null)
                        {
                            PropertyCopier<TParent, MenuAssign>.Copy(myobject, dataobjMenuAssign);
                            dataobjMenuAssign.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "offerbanners":
                        OfferBanner offer = new OfferBanner();
                        PropertyCopier<TParent, OfferBanner>.Copy(myobject, offer);
                        var dataoffer = context.OfferBanners.SingleOrDefault(b => b.Id == offer.Id);
                        // Checking if any such record exist 
                        if (dataoffer != null)
                        {
                            PropertyCopier<TParent, OfferBanner>.Copy(myobject, dataoffer);
                            dataoffer.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "kycremarks":
                        KYCRemark objKYCRemark = new KYCRemark();
                        PropertyCopier<TParent, KYCRemark>.Copy(myobject, objKYCRemark);
                        var dataobjKYCRemark = context.KYCRemarks.SingleOrDefault(b => b.Id == objKYCRemark.Id);
                        // Checking if any such record exist 
                        if (dataobjKYCRemark != null)
                        {
                            PropertyCopier<TParent, KYCRemark>.Copy(myobject, dataobjKYCRemark);
                            dataobjKYCRemark.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "ticketimages":
                        TicketImage objTicketImage = new TicketImage();
                        PropertyCopier<TParent, TicketImage>.Copy(myobject, objTicketImage);
                        var dataobjTicketImage = context.TicketImages.SingleOrDefault(b => b.Id == objTicketImage.Id);
                        // Checking if any such record exist 
                        if (dataobjTicketImage != null)
                        {
                            PropertyCopier<TParent, TicketImage>.Copy(myobject, dataobjTicketImage);
                            dataobjTicketImage.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "tickets":
                        Ticket objTicket = new Ticket();
                        PropertyCopier<TParent, Ticket>.Copy(myobject, objTicket);
                        var dataobjTicket = context.Tickets.SingleOrDefault(b => b.Id == objTicket.Id);
                        // Checking if any such record exist 
                        if (dataobjTicket != null)
                        {
                            PropertyCopier<TParent, Ticket>.Copy(myobject, dataobjTicket);
                            dataobjTicket.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;

                    case "balancehistory":
                        BalanceHistory objbalhis = new BalanceHistory();
                        PropertyCopier<TParent, BalanceHistory>.Copy(myobject, objbalhis);
                        var dataobjbalhis = context.BalanceHistories.SingleOrDefault(b => b.Id == objbalhis.Id);
                        // Checking if any such record exist 
                        if (dataobjbalhis != null)
                        {
                            PropertyCopier<TParent, BalanceHistory>.Copy(myobject, dataobjbalhis);
                            dataobjbalhis.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "ticketsreply":
                        TicketsReply objTicketsReply = new TicketsReply();
                        PropertyCopier<TParent, TicketsReply>.Copy(myobject, objTicketsReply);
                        var dataobjTicketsReply = context.TicketsReplies.SingleOrDefault(b => b.Id == objTicketsReply.Id);
                        // Checking if any such record exist 
                        if (dataobjTicketsReply != null)
                        {
                            PropertyCopier<TParent, TicketsReply>.Copy(myobject, dataobjTicketsReply);
                            dataobjTicketsReply.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "ticketscategory":
                        TicketsCategory objTicketsCategory = new TicketsCategory();
                        PropertyCopier<TParent, TicketsCategory>.Copy(myobject, objTicketsCategory);
                        var dataobjTicketsCategory = context.TicketsCategories.SingleOrDefault(b => b.Id == objTicketsCategory.Id);
                        // Checking if any such record exist 
                        if (dataobjTicketsCategory != null)
                        {
                            PropertyCopier<TParent, TicketsCategory>.Copy(myobject, dataobjTicketsCategory);
                            dataobjTicketsCategory.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "adminlogin":
                        AdminLogin objAdminLogin = new AdminLogin();
                        PropertyCopier<TParent, AdminLogin>.Copy(myobject, objAdminLogin);
                        var dataobjAdminLogin = context.AdminLogins.SingleOrDefault(b => b.Id == objAdminLogin.Id);
                        // Checking if any such record exist 
                        if (dataobjAdminLogin != null)
                        {
                            PropertyCopier<TParent, AdminLogin>.Copy(myobject, dataobjAdminLogin);
                            dataobjAdminLogin.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "notification":
                        Notification objNotification = new Notification();
                        PropertyCopier<TParent, Notification>.Copy(myobject, objNotification);
                        var dataobjNotification = context.Notifications.SingleOrDefault(b => b.Id == objNotification.Id);
                        // Checking if any such record exist 
                        if (dataobjNotification != null)
                        {
                            PropertyCopier<TParent, Notification>.Copy(myobject, dataobjNotification);
                            dataobjNotification.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "usersdeviceregistration":
                        UsersDeviceRegistration objUsersDeviceRegistration = new UsersDeviceRegistration();
                        PropertyCopier<TParent, UsersDeviceRegistration>.Copy(myobject, objUsersDeviceRegistration);
                        var dataobjUsersDeviceRegistration = context.UsersDeviceRegistrations.SingleOrDefault(b => b.Id == objUsersDeviceRegistration.Id);
                        // Checking if any such record exist 
                        if (dataobjUsersDeviceRegistration != null)
                        {
                            PropertyCopier<TParent, UsersDeviceRegistration>.Copy(myobject, dataobjUsersDeviceRegistration);
                            dataobjUsersDeviceRegistration.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "redeempoints":
                        RedeemPoint objRedeemPoint = new RedeemPoint();
                        PropertyCopier<TParent, RedeemPoint>.Copy(myobject, objRedeemPoint);
                        var dataRedeemPoint = context.RedeemPoints.SingleOrDefault(b => b.Id == objRedeemPoint.Id);
                        // Checking if any such record exist 
                        if (dataRedeemPoint != null)
                        {
                            PropertyCopier<TParent, RedeemPoint>.Copy(myobject, dataRedeemPoint);
                            dataRedeemPoint.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "userbankdetail":
                        UserBankDetail objUserBankDetail = new UserBankDetail();
                        PropertyCopier<TParent, UserBankDetail>.Copy(myobject, objUserBankDetail);
                        var dataUserBankDetail = context.UserBankDetails.SingleOrDefault(b => b.Id == objUserBankDetail.Id);
                        // Checking if any such record exist 
                        if (dataUserBankDetail != null)
                        {
                            PropertyCopier<TParent, UserBankDetail>.Copy(myobject, dataUserBankDetail);
                            dataUserBankDetail.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "providerservicecategorylist":
                        ProviderServiceCategoryList providerservice = new ProviderServiceCategoryList();
                        PropertyCopier<TParent, ProviderServiceCategoryList>.Copy(myobject, providerservice);
                        var dataproviderservice = context.ProviderServiceCategoryLists.SingleOrDefault(b => b.Id == providerservice.Id);
                        // Checking if any such record exist 
                        if (dataproviderservice != null)
                        {
                            PropertyCopier<TParent, ProviderServiceCategoryList>.Copy(myobject, dataproviderservice);
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "commissionupdatehistory":
                        CommissionUpdateHistory commissionhistory = new CommissionUpdateHistory();
                        PropertyCopier<TParent, CommissionUpdateHistory>.Copy(myobject, commissionhistory);
                        var datacommissionhistory = context.CommissionUpdateHistories.SingleOrDefault(b => b.Id == commissionhistory.Id);
                        // Checking if any such record exist 
                        if (datacommissionhistory != null)
                        {
                            PropertyCopier<TParent, CommissionUpdateHistory>.Copy(myobject, datacommissionhistory);
                            datacommissionhistory.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "userauthorization":
                        UserAuthorization user = new UserAuthorization();
                        PropertyCopier<TParent, UserAuthorization>.Copy(myobject, user);
                        var data = context.UserAuthorizations.SingleOrDefault(b => b.Id == user.Id);
                        if (data != null)
                        {
                            PropertyCopier<TParent, UserAuthorization>.Copy(myobject, data);
                            data.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;

                    case "user":
                        User objuser = new User();
                        PropertyCopier<TParent, User>.Copy(myobject, objuser);
                        var objUserdata = context.Users.SingleOrDefault(b => b.Id == objuser.Id);
                        if (objUserdata != null)
                        {
                            PropertyCopier<TParent, User>.Copy(myobject, objUserdata);
                            objUserdata.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;

                    case "vendor_api_requests":
                        Vendor_API_Requests objVendor_API_Requests = new Vendor_API_Requests();
                        PropertyCopier<TParent, Vendor_API_Requests>.Copy(myobject, objVendor_API_Requests);
                        var dataobjVendor_API_Requests = context.Vendor_API_Requests.SingleOrDefault(b => b.Id == objVendor_API_Requests.Id);
                        if (dataobjVendor_API_Requests != null)
                        {
                            PropertyCopier<TParent, Vendor_API_Requests>.Copy(myobject, dataobjVendor_API_Requests);
                            dataobjVendor_API_Requests.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "remittance_api_requests":
                        Remittance_API_Requests objRemittance_API_Requests = new Remittance_API_Requests();
                        PropertyCopier<TParent, Remittance_API_Requests>.Copy(myobject, objRemittance_API_Requests);
                        var dataobjRemittance_API_Requests = context.Remittance_API_Requests.SingleOrDefault(b => b.Id == objRemittance_API_Requests.Id);
                        if (dataobjRemittance_API_Requests != null)
                        {
                            PropertyCopier<TParent, Remittance_API_Requests>.Copy(myobject, dataobjRemittance_API_Requests);
                            dataobjRemittance_API_Requests.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "verification":
                        Verification verify = new Verification();
                        PropertyCopier<TParent, Verification>.Copy(myobject, verify);
                        var dataverify = context.Verifications.SingleOrDefault(b => b.Id == verify.Id);
                        // Checking if any such record exist 
                        if (dataverify != null)
                        {
                            PropertyCopier<TParent, Verification>.Copy(myobject, dataverify);
                            dataverify.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "logs":
                        log lg = new log();
                        PropertyCopier<TParent, log>.Copy(myobject, lg);
                        var datalog = context.logs.SingleOrDefault(b => b.Id == lg.Id);
                        // Checking if any such record exist 
                        if (datalog != null)
                        {
                            PropertyCopier<TParent, log>.Copy(myobject, datalog);
                            datalog.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "userdocuments":
                        UserDocument doc = new UserDocument();
                        PropertyCopier<TParent, UserDocument>.Copy(myobject, doc);
                        var datadoc = context.UserDocuments.SingleOrDefault(b => b.Id == doc.Id);
                        // Checking if any such record exist 
                        if (datadoc != null)
                        {
                            PropertyCopier<TParent, UserDocument>.Copy(myobject, datadoc);
                            datadoc.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "depositorders":
                        DepositOrder order = new DepositOrder();
                        PropertyCopier<TParent, DepositOrder>.Copy(myobject, order);
                        var dataorder = context.DepositOrders.SingleOrDefault(b => b.Id == order.Id);
                        // Checking if any such record exist 
                        if (dataorder != null)
                        {
                            PropertyCopier<TParent, DepositOrder>.Copy(myobject, dataorder);
                            dataorder.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "settings":
                        Setting setting = new Setting();
                        PropertyCopier<TParent, Setting>.Copy(myobject, setting);
                        var datasetting = context.Settings.SingleOrDefault(b => b.Id == setting.Id);
                        // Checking if any such record exist 
                        if (datasetting != null)
                        {
                            PropertyCopier<TParent, Setting>.Copy(myobject, datasetting);
                            datasetting.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "commission":
                        Commission commision = new Commission();
                        PropertyCopier<TParent, Commission>.Copy(myobject, commision);
                        var datacommision = context.Commissions.SingleOrDefault(b => b.Id == commision.Id);
                        // Checking if any such record exist 
                        if (datacommision != null)
                        {
                            PropertyCopier<TParent, Commission>.Copy(myobject, datacommision);
                            datacommision.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "kycstatushistory":
                        KYCStatusHistory kYCStatusHistory = new KYCStatusHistory();
                        PropertyCopier<TParent, KYCStatusHistory>.Copy(myobject, kYCStatusHistory);
                        var datakYCStatusHistory = context.KYCStatusHistories.SingleOrDefault(b => b.Id == kYCStatusHistory.Id);
                        // Checking if any such record exist 
                        if (datakYCStatusHistory != null)
                        {
                            PropertyCopier<TParent, KYCStatusHistory>.Copy(myobject, datakYCStatusHistory);
                            datakYCStatusHistory.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "requestfund":
                        AddRequestFund objAddRequestFund = new AddRequestFund();
                        PropertyCopier<TParent, AddRequestFund>.Copy(myobject, objAddRequestFund);
                        var dataobjAddRequestFund = context.Request_Fund.SingleOrDefault(b => b.Id == objAddRequestFund.Id);
                        // Checking if any such record exist 
                        if (dataobjAddRequestFund != null)
                        {
                            PropertyCopier<TParent, Request_Fund>.Copy(myobject, dataobjAddRequestFund);
                            dataobjAddRequestFund.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "votingcompetition":
                        VotingCompetition objvotingcomp = new VotingCompetition();
                        PropertyCopier<TParent, VotingCompetition>.Copy(myobject, objvotingcomp);
                        var dataobjvotingcomp = context.VotingCompetitions.SingleOrDefault(b => b.Id == objvotingcomp.Id);
                        // Checking if any such record exist 
                        if (dataobjvotingcomp != null)
                        {
                            PropertyCopier<TParent, VotingCompetition>.Copy(myobject, dataobjvotingcomp);
                            dataobjvotingcomp.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "votingcandidate":
                        VotingCandidate objvotingcandidate = new VotingCandidate();
                        PropertyCopier<TParent, VotingCandidate>.Copy(myobject, objvotingcandidate);
                        var dataobjvotingcandidate = context.VotingCandidates.SingleOrDefault(b => b.Id == objvotingcandidate.Id);
                        // Checking if any such record exist 
                        if (dataobjvotingcandidate != null)
                        {
                            PropertyCopier<TParent, VotingCandidate>.Copy(myobject, dataobjvotingcandidate);
                            dataobjvotingcandidate.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "votingpackages":
                        VotingPackage objvotingpackage = new VotingPackage();
                        PropertyCopier<TParent, VotingPackage>.Copy(myobject, objvotingpackage);
                        var dataobjvotingpackage = context.VotingPackages.SingleOrDefault(b => b.Id == objvotingpackage.Id);
                        // Checking if any such record exist 
                        if (dataobjvotingpackage != null)
                        {
                            PropertyCopier<TParent, VotingPackage>.Copy(myobject, dataobjvotingpackage);
                            dataobjvotingpackage.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "votinglist":
                        VotingList objvotinglist = new VotingList();
                        PropertyCopier<TParent, VotingList>.Copy(myobject, objvotinglist);
                        var dataobjvotinglist = context.VotingLists.SingleOrDefault(b => b.Id == objvotinglist.Id);
                        // Checking if any such record exist 
                        if (dataobjvotinglist != null)
                        {
                            PropertyCopier<TParent, VotingList>.Copy(myobject, dataobjvotinglist);
                            dataobjvotinglist.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "marque":
                        Marque objMarque = new Marque();
                        PropertyCopier<TParent, Marque>.Copy(myobject, objMarque);
                        var dataobMarque = context.Marques.SingleOrDefault(b => b.Id == objMarque.Id);
                        // Checking if any such record exist 
                        if (dataobMarque != null)
                        {
                            PropertyCopier<TParent, Marque>.Copy(myobject, dataobMarque);
                            dataobMarque.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "purpose":
                        Purpose objpurpose = new Purpose();
                        PropertyCopier<TParent, Purpose>.Copy(myobject, objpurpose);
                        var dataobjpurpose = context.Purposes.SingleOrDefault(b => b.Id == objpurpose.Id);
                        // Checking if any such record exist 
                        if (dataobjpurpose != null)
                        {
                            PropertyCopier<TParent, Purpose>.Copy(myobject, dataobjpurpose);
                            dataobjpurpose.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "occupation":
                        Occupation objOccupation = new Occupation();
                        PropertyCopier<TParent, Occupation>.Copy(myobject, objOccupation);
                        var dataobjOccupation = context.Occupations.SingleOrDefault(b => b.Id == objOccupation.Id);
                        // Checking if any such record exist 
                        if (dataobjOccupation != null)
                        {
                            PropertyCopier<TParent, Occupation>.Copy(myobject, dataobjOccupation);
                            dataobjOccupation.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "transactionlimit":
                        TransactionLimit objTransactionLimit = new TransactionLimit();
                        PropertyCopier<TParent, TransactionLimit>.Copy(myobject, objTransactionLimit);
                        var dataobjTransactionLimit = context.TransactionLimits.SingleOrDefault(b => b.Id == objTransactionLimit.Id);
                        // Checking if any such record exist 
                        if (dataobjTransactionLimit != null)
                        {
                            PropertyCopier<TParent, TransactionLimit>.Copy(myobject, dataobjTransactionLimit);
                            dataobjTransactionLimit.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "banktransactions":
                        BankTransaction objBankTransaction = new BankTransaction();
                        PropertyCopier<TParent, BankTransaction>.Copy(myobject, objBankTransaction);
                        var dataobjBankTransaction = context.BankTransactions.SingleOrDefault(b => b.Id == objBankTransaction.Id);
                        // Checking if any such record exist 
                        if (dataobjBankTransaction != null)
                        {
                            PropertyCopier<TParent, BankTransaction>.Copy(myobject, dataobjBankTransaction);
                            dataobjBankTransaction.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "flightbookingdetails":
                        FlightBookingDetail objFlightBookingDetail = new FlightBookingDetail();
                        PropertyCopier<TParent, FlightBookingDetail>.Copy(myobject, objFlightBookingDetail);
                        var dataobjFlightBookingDetail = context.FlightBookingDetails.SingleOrDefault(b => b.Id == objFlightBookingDetail.Id);
                        // Checking if any such record exist 
                        if (dataobjFlightBookingDetail != null)
                        {
                            PropertyCopier<TParent, FlightBookingDetail>.Copy(myobject, dataobjFlightBookingDetail);
                            dataobjFlightBookingDetail.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "flightpassengersdetails":
                        FlightPassengersDetail objFlightPassengersDetail = new FlightPassengersDetail();
                        PropertyCopier<TParent, FlightPassengersDetail>.Copy(myobject, objFlightPassengersDetail);
                        var dataobjFlightPassengersDetail = context.FlightPassengersDetails.SingleOrDefault(b => b.Id == objFlightPassengersDetail.Id);
                        // Checking if any such record exist 
                        if (dataobjFlightPassengersDetail != null)
                        {
                            PropertyCopier<TParent, FlightPassengersDetail>.Copy(myobject, dataobjFlightPassengersDetail);
                            dataobjFlightPassengersDetail.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "merchantorders":
                        MerchantOrder objMerchantOrder = new MerchantOrder();
                        PropertyCopier<TParent, MerchantOrder>.Copy(myobject, objMerchantOrder);
                        var dataobjMerchantOrder = context.MerchantOrders.SingleOrDefault(b => b.Id == objMerchantOrder.Id);
                        // Checking if any such record exist 
                        if (dataobjMerchantOrder != null)
                        {
                            PropertyCopier<TParent, MerchantOrder>.Copy(myobject, dataobjMerchantOrder);
                            dataobjMerchantOrder.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "giftmpcoinshistory":
                        GiftMPCoinsHistory objmpcoinshistory = new GiftMPCoinsHistory();
                        PropertyCopier<TParent, GiftMPCoinsHistory>.Copy(myobject, objmpcoinshistory);
                        var dataobjmpcoinshistory = context.GiftMPCoinsHistories.SingleOrDefault(b => b.Id == objmpcoinshistory.Id);
                        // Checking if any such record exist 
                        if (dataobjmpcoinshistory != null)
                        {
                            PropertyCopier<TParent, GiftMPCoinsHistory>.Copy(myobject, dataobjmpcoinshistory);
                            dataobjmpcoinshistory.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "providerlogoslist":
                        ProviderLogosList objProviderLogosList = new ProviderLogosList();
                        PropertyCopier<TParent, ProviderLogosList>.Copy(myobject, objProviderLogosList);
                        var dataobjProviderLogosList = context.ProviderLogosLists.SingleOrDefault(b => b.Id == objProviderLogosList.Id);
                        // Checking if any such record exist 
                        if (dataobjProviderLogosList != null)
                        {
                            PropertyCopier<TParent, ProviderLogosList>.Copy(myobject, dataobjProviderLogosList);
                            //dataobjProviderLogosList.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "notificationcampaignexcel":
                        NotificationCampaignExcel objNotificationCampaignExcel = new NotificationCampaignExcel();
                        PropertyCopier<TParent, NotificationCampaignExcel>.Copy(myobject, objNotificationCampaignExcel);
                        var dataobjNotificationCampaignExcel = context.NotificationCampaignExcels.SingleOrDefault(b => b.Id == objNotificationCampaignExcel.Id);
                        // Checking if any such record exist 
                        if (dataobjNotificationCampaignExcel != null)
                        {
                            PropertyCopier<TParent, NotificationCampaignExcel>.Copy(myobject, dataobjNotificationCampaignExcel);
                            dataobjNotificationCampaignExcel.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "dealsandoffers":
                        DealsAndOffer objDealsAndOffer = new DealsAndOffer();
                        PropertyCopier<TParent, DealsAndOffer>.Copy(myobject, objDealsAndOffer);
                        var dataobjDealsAndOffer = context.DealsAndOffers.SingleOrDefault(b => b.Id == objDealsAndOffer.Id);
                        // Checking if any such record exist 
                        if (dataobjDealsAndOffer != null)
                        {
                            PropertyCopier<TParent, DealsAndOffer>.Copy(myobject, dataobjDealsAndOffer);
                            dataobjDealsAndOffer.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    case "couponshistory":
                        CouponsHistory objCouponsHistory = new CouponsHistory();
                        PropertyCopier<TParent, CouponsHistory>.Copy(myobject, objCouponsHistory);
                        var dataCouponsHistory = context.CouponsHistories.SingleOrDefault(b => b.Id == objCouponsHistory.Id);
                        // Checking if any such record exist 
                        if (dataCouponsHistory != null)
                        {
                            PropertyCopier<TParent, CouponsHistory>.Copy(myobject, dataCouponsHistory);
                            dataCouponsHistory.UpdatedDate = System.DateTime.UtcNow;
                            context.SaveChanges();
                            dbcontext.Commit();
                        }
                        objret = true;
                        break;
                    default:
                        objret = false;
                        break;
                }

            }
            catch (DbEntityValidationException e)
            {
                string errmsg = string.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {
                    errmsg = errmsg + string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        errmsg = errmsg + string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                        Common.AddLogs(errmsg, true, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false, "", "", 0, Common.CreatedBy, Common.CreatedByName);
                    }
                }
                dbcontext.Rollback();
                throw;
            }
            catch (Exception ex)
            {
                Common.AddLogs(ex.Message, true, (int)AddLog.LogType.DBLogs, Common.CreatedBy, Common.CreatedByName, false, "", "", 0, Common.CreatedBy, Common.CreatedByName);
                dbcontext.Rollback();
                throw;
            }
            return objret;
        }
        private static Int64 RepoInsert(TParent myobject, string TableName, MyPayEntities context, DbContextTransaction dbcontext)
        {
            Int64 objret = 0;
            try
            {

                switch (TableName.ToLower())
                {
                    case "menus":
                        Menu objItem_Menu = new Menu();
                        PropertyCopier<TParent, Menu>.Copy(myobject, objItem_Menu);
                        objItem_Menu.Id = 0;
                        context.Menus.Add(objItem_Menu);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_Menu.Id;
                        break;
                    case "balancehistorymerchant":
                        BalanceHistoryMerchant objItem = new BalanceHistoryMerchant();
                        PropertyCopier<TParent, BalanceHistoryMerchant>.Copy(myobject, objItem);
                        objItem.Id = 0;
                        context.BalanceHistoryMerchants.Add(objItem);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem.Id;
                        break;

                    case "cable_car":
                        Cable_Car cable = new Cable_Car();
                        PropertyCopier<TParent, Cable_Car>.Copy(myobject, cable);
                        cable.CableId = 0;
                        context.Cable_Car.Add(cable);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = cable.CableId;
                        break;
                    case "cablecar_ticketinvoice":
                        CableCar_TicketInVoice CCTI = new CableCar_TicketInVoice();
                        PropertyCopier<TParent, CableCar_TicketInVoice>.Copy(myobject, CCTI);
                        CCTI.TicketId = 0;
                        context.CableCar_TicketInVoice.Add(CCTI);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = CCTI.TicketId;
                        break;

                    case "plasmatechissueticketresdetail":
                        PlasmaTechIssueTicketResDetail objItem_PlasmaTech = new PlasmaTechIssueTicketResDetail();
                        PropertyCopier<TParent, PlasmaTechIssueTicketResDetail>.Copy(myobject, objItem_PlasmaTech);
                        objItem_PlasmaTech.Id = 0;
                        long gid = long.Parse(DateTime.Now.ToString("ssffff"));
                        objItem_PlasmaTech.Issued_Id = gid;
                        context.PlasmaTechIssueTicketResDetails.Add(objItem_PlasmaTech);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_PlasmaTech.Id;
                        break;

                    case "merchantwithdrawalrequest":
                        MerchantWithdrawalRequest objItem_MerchantWithdrawalRequest = new MerchantWithdrawalRequest();
                        PropertyCopier<TParent, MerchantWithdrawalRequest>.Copy(myobject, objItem_MerchantWithdrawalRequest);
                        objItem_MerchantWithdrawalRequest.Id = 0;
                        context.MerchantWithdrawalRequests.Add(objItem_MerchantWithdrawalRequest);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_MerchantWithdrawalRequest.Id;
                        break;
                    case "apisettingshistory":
                        ApiSettingsHistory objItem_ApiSettingsHistory = new ApiSettingsHistory();
                        PropertyCopier<TParent, ApiSettingsHistory>.Copy(myobject, objItem_ApiSettingsHistory);
                        objItem_ApiSettingsHistory.Id = 0;
                        context.ApiSettingsHistories.Add(objItem_ApiSettingsHistory);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_ApiSettingsHistory.Id;
                        break;
                    case "merchantbankdetail":
                        MerchantBankDetail objItem_Merchantbank = new MerchantBankDetail();
                        PropertyCopier<TParent, MerchantBankDetail>.Copy(myobject, objItem_Merchantbank);
                        objItem_Merchantbank.Id = 0;
                        context.MerchantBankDetails.Add(objItem_Merchantbank);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_Merchantbank.Id;
                        break;
                    case "merchant":
                        Merchant objItem_Merchant = new Merchant();
                        PropertyCopier<TParent, Merchant>.Copy(myobject, objItem_Merchant);
                        objItem_Merchant.Id = 0;
                        context.Merchants.Add(objItem_Merchant);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_Merchant.Id;
                        break;
                    case "datapackdetail":
                        DataPackDetail objItem_datapack = new DataPackDetail();
                        PropertyCopier<TParent, DataPackDetail>.Copy(myobject, objItem_datapack);
                        objItem_datapack.Id = 0;
                        context.DataPackDetails.Add(objItem_datapack);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_datapack.Id;
                        break;
                    case "notificationcampaign":
                        NotificationCampaign objItem_NotificationCampaign = new NotificationCampaign();
                        PropertyCopier<TParent, NotificationCampaign>.Copy(myobject, objItem_NotificationCampaign);
                        objItem_NotificationCampaign.Id = 0;
                        context.NotificationCampaigns.Add(objItem_NotificationCampaign);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_NotificationCampaign.Id;
                        break;
                    case "insurancedetail":
                        InsuranceDetail objItem_InsuranceDetail = new InsuranceDetail();
                        PropertyCopier<TParent, InsuranceDetail>.Copy(myobject, objItem_InsuranceDetail);
                        objItem_InsuranceDetail.Id = 0;
                        context.InsuranceDetails.Add(objItem_InsuranceDetail);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_InsuranceDetail.Id;
                        break;
                    case "sharereferlink":
                        ShareReferLink objItem_ShareReferLink = new ShareReferLink();
                        PropertyCopier<TParent, ShareReferLink>.Copy(myobject, objItem_ShareReferLink);
                        objItem_ShareReferLink.Id = 0;
                        context.ShareReferLinks.Add(objItem_ShareReferLink);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_ShareReferLink.Id;
                        break;
                    case "exportdata":
                        ExportData objItem_ExportData = new ExportData();
                        PropertyCopier<TParent, ExportData>.Copy(myobject, objItem_ExportData);
                        objItem_ExportData.Id = 0;
                        context.ExportDatas.Add(objItem_ExportData);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_ExportData.Id;
                        break;
                    case "giftcashbackhistory":
                        GiftCashbackHistory objItem_GiftCashbackHistory = new GiftCashbackHistory();
                        PropertyCopier<TParent, GiftCashbackHistory>.Copy(myobject, objItem_GiftCashbackHistory);
                        objItem_GiftCashbackHistory.Id = 0;
                        context.GiftCashbackHistories.Add(objItem_GiftCashbackHistory);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_GiftCashbackHistory.Id;
                        break;
                    case "adminlogin":
                        AdminLogin objItem_AdminLogin = new AdminLogin();
                        PropertyCopier<TParent, AdminLogin>.Copy(myobject, objItem_AdminLogin);
                        objItem_AdminLogin.Id = 0;
                        context.AdminLogins.Add(objItem_AdminLogin);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_AdminLogin.Id;
                        break;
                    case "roles":
                        Role objItem_Role = new Role();
                        PropertyCopier<TParent, Role>.Copy(myobject, objItem_Role);
                        objItem_Role.Id = 0;
                        context.Roles.Add(objItem_Role);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_Role.Id;
                        break;
                    case "menuassign":
                        MenuAssign objItem_MenuAssign = new MenuAssign();
                        PropertyCopier<TParent, MenuAssign>.Copy(myobject, objItem_MenuAssign);
                        objItem_MenuAssign.Id = 0;
                        context.MenuAssigns.Add(objItem_MenuAssign);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_MenuAssign.Id;
                        break;
                    case "offerbanners":
                        OfferBanner objItem_offer = new OfferBanner();
                        PropertyCopier<TParent, OfferBanner>.Copy(myobject, objItem_offer);
                        objItem_offer.Id = 0;
                        context.OfferBanners.Add(objItem_offer);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_offer.Id;
                        break;
                    case "kycremarks":
                        KYCRemark objItem_KYCRemark = new KYCRemark();
                        PropertyCopier<TParent, KYCRemark>.Copy(myobject, objItem_KYCRemark);
                        objItem_KYCRemark.Id = 0;
                        context.KYCRemarks.Add(objItem_KYCRemark);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_KYCRemark.Id;
                        break;
                    case "ticketimages":
                        TicketImage objItem_TicketImage = new TicketImage();
                        PropertyCopier<TParent, TicketImage>.Copy(myobject, objItem_TicketImage);
                        objItem_TicketImage.Id = 0;
                        context.TicketImages.Add(objItem_TicketImage);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_TicketImage.Id;
                        break;
                    case "tickets":
                        Ticket objItem_Ticket = new Ticket();
                        PropertyCopier<TParent, Ticket>.Copy(myobject, objItem_Ticket);
                        objItem_Ticket.Id = 0;
                        context.Tickets.Add(objItem_Ticket);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_Ticket.Id;
                        break;
                    case "balancehistory":
                        BalanceHistory objItem_Balhis = new BalanceHistory();
                        PropertyCopier<TParent, BalanceHistory>.Copy(myobject, objItem_Balhis);
                        objItem_Balhis.Id = 0;
                        context.BalanceHistories.Add(objItem_Balhis);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_Balhis.Id;
                        break;
                    case "ticketsreply":
                        TicketsReply objItem_TicketsReply = new TicketsReply();
                        PropertyCopier<TParent, TicketsReply>.Copy(myobject, objItem_TicketsReply);
                        objItem_TicketsReply.Id = 0;
                        context.TicketsReplies.Add(objItem_TicketsReply);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_TicketsReply.Id;
                        break;
                    case "ticketscategory":
                        TicketsCategory objItem_TicketsCategory = new TicketsCategory();
                        PropertyCopier<TParent, TicketsCategory>.Copy(myobject, objItem_TicketsCategory);
                        objItem_TicketsCategory.Id = 0;
                        context.TicketsCategories.Add(objItem_TicketsCategory);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_TicketsCategory.Id;
                        break;
                    case "notification":
                        Notification objItem_Notification = new Notification();
                        PropertyCopier<TParent, Notification>.Copy(myobject, objItem_Notification);
                        objItem_Notification.Id = 0;
                        context.Notifications.Add(objItem_Notification);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_Notification.Id;
                        break;
                    case "usersdeviceregistration":
                        UsersDeviceRegistration objItem_UsersDeviceRegistration = new UsersDeviceRegistration();
                        PropertyCopier<TParent, UsersDeviceRegistration>.Copy(myobject, objItem_UsersDeviceRegistration);
                        objItem_UsersDeviceRegistration.Id = 0;
                        context.UsersDeviceRegistrations.Add(objItem_UsersDeviceRegistration);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_UsersDeviceRegistration.Id;
                        break;
                    case "transactionlimit":
                        TransactionLimit objItem_TransactionLimit = new TransactionLimit();
                        PropertyCopier<TParent, TransactionLimit>.Copy(myobject, objItem_TransactionLimit);
                        objItem_TransactionLimit.Id = 0;
                        context.TransactionLimits.Add(objItem_TransactionLimit);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_TransactionLimit.Id;
                        break;
                    case "redeempoints":
                        RedeemPoint objItem_RedeemPoint = new RedeemPoint();
                        PropertyCopier<TParent, RedeemPoint>.Copy(myobject, objItem_RedeemPoint);
                        objItem_RedeemPoint.Id = 0;
                        context.RedeemPoints.Add(objItem_RedeemPoint);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_RedeemPoint.Id;
                        break;
                    case "userbankdetail":
                        UserBankDetail objItem_UserBankDetail = new UserBankDetail();
                        PropertyCopier<TParent, UserBankDetail>.Copy(myobject, objItem_UserBankDetail);
                        objItem_UserBankDetail.Id = 0;
                        context.UserBankDetails.Add(objItem_UserBankDetail);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_UserBankDetail.Id;
                        break;
                    case "settingshistory":
                        SettingsHistory objItem_settinghistory = new SettingsHistory();
                        PropertyCopier<TParent, SettingsHistory>.Copy(myobject, objItem_settinghistory);
                        objItem_settinghistory.Id = 0;
                        context.SettingsHistories.Add(objItem_settinghistory);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_settinghistory.Id;
                        break;
                    case "commissionupdatehistory":
                        CommissionUpdateHistory objItem_chistory = new CommissionUpdateHistory();
                        PropertyCopier<TParent, CommissionUpdateHistory>.Copy(myobject, objItem_chistory);
                        objItem_chistory.Id = 0;
                        context.CommissionUpdateHistories.Add(objItem_chistory);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_chistory.Id;
                        break;
                    case "userauthorization":
                        UserAuthorization objItem_userauthorization = new UserAuthorization();
                        PropertyCopier<TParent, UserAuthorization>.Copy(myobject, objItem_userauthorization);
                        objItem_userauthorization.Id = 0;
                        context.UserAuthorizations.Add(objItem_userauthorization);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_userauthorization.Id;
                        break;

                    case "user":
                        User objItem_user = new User();
                        PropertyCopier<TParent, User>.Copy(myobject, objItem_user);
                        context.Users.Add(objItem_user);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_user.Id;
                        break;
                    case "vendor_api_requests":
                        Vendor_API_Requests objVendor_API_Requests = new Vendor_API_Requests();
                        PropertyCopier<TParent, Vendor_API_Requests>.Copy(myobject, objVendor_API_Requests);
                        context.Vendor_API_Requests.Add(objVendor_API_Requests);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objVendor_API_Requests.Id;
                        break;

                    case "remittance_api_requests":
                        Remittance_API_Requests objRemittance_API_Requests = new Remittance_API_Requests();
                        PropertyCopier<TParent, Remittance_API_Requests>.Copy(myobject, objRemittance_API_Requests);
                        context.Remittance_API_Requests.Add(objRemittance_API_Requests);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objRemittance_API_Requests.Id;
                        break;

                    case "verification":
                        Verification objItem_verification = new Verification();
                        PropertyCopier<TParent, Verification>.Copy(myobject, objItem_verification);
                        objItem_verification.Id = 0;
                        context.Verifications.Add(objItem_verification);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_verification.Id;

                        break;
                    case "logs":
                        log objItem_log = new log();
                        PropertyCopier<TParent, log>.Copy(myobject, objItem_log);
                        objItem_log.Id = 0;
                        context.logs.Add(objItem_log);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_log.Id;

                        break;
                    case "userdocuments":
                        UserDocument objItem_doc = new UserDocument();
                        PropertyCopier<TParent, UserDocument>.Copy(myobject, objItem_doc);
                        objItem_doc.Id = 0;
                        context.UserDocuments.Add(objItem_doc);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_doc.Id;

                        break;
                    case "depositorders":
                        DepositOrder objItem_deposit = new DepositOrder();
                        PropertyCopier<TParent, DepositOrder>.Copy(myobject, objItem_deposit);
                        objItem_deposit.Id = 0;
                        context.DepositOrders.Add(objItem_deposit);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_deposit.Id;

                        break;
                    case "settings":
                        Setting objItem_setting = new Setting();
                        PropertyCopier<TParent, Setting>.Copy(myobject, objItem_setting);
                        objItem_setting.Id = 0;
                        context.Settings.Add(objItem_setting);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_setting.Id;

                        break;
                    case "commission":
                        Commission objItem_commission = new Commission();
                        PropertyCopier<TParent, Commission>.Copy(myobject, objItem_commission);
                        objItem_commission.Id = 0;
                        context.Commissions.Add(objItem_commission);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_commission.Id;

                        break;
                    case "feedback":
                        Feedback objItem_Feedback = new Feedback();
                        PropertyCopier<TParent, Feedback>.Copy(myobject, objItem_Feedback);
                        objItem_Feedback.Id = 0;
                        context.Feedbacks.Add(objItem_Feedback);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_Feedback.Id;

                        break;
                    case "requestfund":
                        Request_Fund objItem_Request_Fund = new Request_Fund();
                        PropertyCopier<TParent, Request_Fund>.Copy(myobject, objItem_Request_Fund);
                        objItem_Request_Fund.Id = 0;
                        context.Request_Fund.Add(objItem_Request_Fund);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_Request_Fund.Id;
                        objret = 1;
                        break;
                    case "kycstatushistory":
                        KYCStatusHistory objItem_kycstatusHistory = new KYCStatusHistory();
                        PropertyCopier<TParent, KYCStatusHistory>.Copy(myobject, objItem_kycstatusHistory);
                        objItem_kycstatusHistory.Id = 0;
                        context.KYCStatusHistories.Add(objItem_kycstatusHistory);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_kycstatusHistory.Id;
                        break;
                    case "votingcompetition":
                        VotingCompetition objItem_votingcompetition = new VotingCompetition();
                        PropertyCopier<TParent, VotingCompetition>.Copy(myobject, objItem_votingcompetition);
                        objItem_votingcompetition.Id = 0;
                        context.VotingCompetitions.Add(objItem_votingcompetition);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_votingcompetition.Id;
                        break;
                    case "votingcandidate":
                        VotingCandidate objItem_votingcandidate = new VotingCandidate();
                        PropertyCopier<TParent, VotingCandidate>.Copy(myobject, objItem_votingcandidate);
                        objItem_votingcandidate.Id = 0;
                        context.VotingCandidates.Add(objItem_votingcandidate);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_votingcandidate.Id;
                        break;
                    case "votingpackages":
                        VotingPackage objItem_votingpackage = new VotingPackage();
                        PropertyCopier<TParent, VotingPackage>.Copy(myobject, objItem_votingpackage);
                        objItem_votingpackage.Id = 0;
                        context.VotingPackages.Add(objItem_votingpackage);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_votingpackage.Id;
                        break;
                    case "votinglist":
                        VotingList objItem_votinglist = new VotingList();
                        PropertyCopier<TParent, VotingList>.Copy(myobject, objItem_votinglist);
                        objItem_votinglist.Id = 0;
                        context.VotingLists.Add(objItem_votinglist);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_votinglist.Id;
                        break;
                    case "marque":
                        Marque objItem_Marque = new Marque();
                        PropertyCopier<TParent, Marque>.Copy(myobject, objItem_Marque);
                        objItem_Marque.Id = 0;
                        context.Marques.Add(objItem_Marque);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_Marque.Id;
                        break;
                    case "purpose":
                        Purpose objItem_purpose = new Purpose();
                        PropertyCopier<TParent, Purpose>.Copy(myobject, objItem_purpose);
                        objItem_purpose.Id = 0;
                        context.Purposes.Add(objItem_purpose);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_purpose.Id;
                        break;
                    case "occupation":
                        Occupation objItem_occupation = new Occupation();
                        PropertyCopier<TParent, Occupation>.Copy(myobject, objItem_occupation);
                        objItem_occupation.Id = 0;
                        context.Occupations.Add(objItem_occupation);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_occupation.Id;
                        break;
                    case "transactionlimithistory":
                        TransactionLimitHistory objItem_TransactionLimitHistory = new TransactionLimitHistory();
                        PropertyCopier<TParent, TransactionLimitHistory>.Copy(myobject, objItem_TransactionLimitHistory);
                        objItem_TransactionLimitHistory.Id = 0;
                        context.TransactionLimitHistories.Add(objItem_TransactionLimitHistory);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_TransactionLimitHistory.Id;
                        break;
                    case "banktransactions":
                        BankTransaction objItem_BankTransaction = new BankTransaction();
                        PropertyCopier<TParent, BankTransaction>.Copy(myobject, objItem_BankTransaction);
                        objItem_BankTransaction.Id = 0;
                        context.BankTransactions.Add(objItem_BankTransaction);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_BankTransaction.Id;
                        break;
                    case "flightbookingdetails":
                        FlightBookingDetail objItem_FlightBookingDetails = new FlightBookingDetail();
                        PropertyCopier<TParent, FlightBookingDetail>.Copy(myobject, objItem_FlightBookingDetails);
                        objItem_FlightBookingDetails.Id = 0;
                        context.FlightBookingDetails.Add(objItem_FlightBookingDetails);
                        context.SaveChanges();
                        dbcontext.Commit();

                        objret = objItem_FlightBookingDetails.Id;
                        break;
                    case "flightpassengersdetails":
                        FlightPassengersDetail objItem_FlightPassengersDetail = new FlightPassengersDetail();
                        PropertyCopier<TParent, FlightPassengersDetail>.Copy(myobject, objItem_FlightPassengersDetail);
                        objItem_FlightPassengersDetail.Id = 0;
                        context.FlightPassengersDetails.Add(objItem_FlightPassengersDetail);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_FlightPassengersDetail.Id;
                        break;
                    case "userinactiveremarks":
                        UserInActiveRemark objItem_usr = new UserInActiveRemark();
                        PropertyCopier<TParent, UserInActiveRemark>.Copy(myobject, objItem_usr);
                        objItem_usr.Id = 0;
                        context.UserInActiveRemarks.Add(objItem_usr);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_usr.Id;
                        break;
                    case "merchantorders":
                        MerchantOrder objItem_MerchantOrder = new MerchantOrder();
                        PropertyCopier<TParent, MerchantOrder>.Copy(myobject, objItem_MerchantOrder);
                        objItem_MerchantOrder.Id = 0;
                        context.MerchantOrders.Add(objItem_MerchantOrder);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_MerchantOrder.Id;
                        break;
                    case "giftmpcoinshistory":
                        GiftMPCoinsHistory objItem_GiftMPCoinsHistory = new GiftMPCoinsHistory();
                        PropertyCopier<TParent, GiftMPCoinsHistory>.Copy(myobject, objItem_GiftMPCoinsHistory);
                        objItem_GiftMPCoinsHistory.Id = 0;
                        context.GiftMPCoinsHistories.Add(objItem_GiftMPCoinsHistory);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_GiftMPCoinsHistory.Id;
                        break;
                    case "coupons":
                        Coupon objItem_Coupon = new Coupon();
                        PropertyCopier<TParent, Coupon>.Copy(myobject, objItem_Coupon);
                        objItem_Coupon.Id = 0;
                        context.Coupons.Add(objItem_Coupon);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_Coupon.Id;
                        break;
                    case "notificationcampaignexcel":
                        NotificationCampaignExcel objItem_NotificationCampaignExcel = new NotificationCampaignExcel();
                        PropertyCopier<TParent, NotificationCampaignExcel>.Copy(myobject, objItem_NotificationCampaignExcel);
                        objItem_NotificationCampaignExcel.Id = 0;
                        context.NotificationCampaignExcels.Add(objItem_NotificationCampaignExcel);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_NotificationCampaignExcel.Id;
                        break;
                    case "notificationcampaignexcelhistory":
                        NotificationCampaignExcelHistory objItem_NotificationCampaignExcelHistory = new NotificationCampaignExcelHistory();
                        PropertyCopier<TParent, NotificationCampaignExcelHistory>.Copy(myobject, objItem_NotificationCampaignExcelHistory);
                        objItem_NotificationCampaignExcelHistory.Id = 0;
                        context.NotificationCampaignExcelHistories.Add(objItem_NotificationCampaignExcelHistory);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_NotificationCampaignExcelHistory.Id;
                        break;
                    case "estatementpdftoken":
                        EStatementPDFToken objItem_EStatementPDFToken = new EStatementPDFToken();
                        PropertyCopier<TParent, EStatementPDFToken>.Copy(myobject, objItem_EStatementPDFToken);
                        objItem_EStatementPDFToken.Id = 0;
                        context.EStatementPDFTokens.Add(objItem_EStatementPDFToken);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_EStatementPDFToken.Id;
                        break;
                    case "dealsandoffers":
                        DealsAndOffer objItem_DealsAndOffer = new DealsAndOffer();
                        PropertyCopier<TParent, DealsAndOffer>.Copy(myobject, objItem_DealsAndOffer);
                        objItem_DealsAndOffer.Id = 0;
                        context.DealsAndOffers.Add(objItem_DealsAndOffer);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_DealsAndOffer.Id;
                        break;
                    case "couponshistory":
                        CouponsHistory objItem_CouponsHistory = new CouponsHistory();
                        PropertyCopier<TParent, CouponsHistory>.Copy(myobject, objItem_CouponsHistory);
                        objItem_CouponsHistory.Id = 0;
                        context.CouponsHistories.Add(objItem_CouponsHistory);
                        context.SaveChanges();
                        dbcontext.Commit();
                        objret = objItem_CouponsHistory.Id;
                        break;
                    //case "airline_commissions":
                    //    CouponsHistory objItem_CouponsHistory = new CouponsHistory();
                    //    PropertyCopier<TParent, CouponsHistory>.Copy(myobject, objItem_CouponsHistory);
                    //    objItem_CouponsHistory.Id = 0;
                    //    context.CouponsHistories.Add(objItem_CouponsHistory);
                    //    context.SaveChanges();
                    //    dbcontext.Commit();
                    //    objret = objItem_CouponsHistory.Id;
                    //    break;
                    default:
                        objret = 0;
                        break;
                }
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
                dbcontext.Rollback();
                throw;
            }
            catch (Exception ex)
            {
                dbcontext.Rollback();
                throw;
            }
            return objret;


        }

        private static Int64 RepoInsertList(List<TParent> myobject, string TableName, MyPayEntities context)
        {
            Int64 objret = 0;
            try
            {
                switch (TableName.ToLower())
                {
                    case "flightbookingdetails":
                        foreach (var myobjectItem in myobject)
                        {
                            FlightBookingDetail objItem_FlightBookingDetail = new FlightBookingDetail();
                            PropertyCopier<TParent, FlightBookingDetail>.Copy(myobjectItem, objItem_FlightBookingDetail);
                            objItem_FlightBookingDetail.Id = 0;
                            context.FlightBookingDetails.Add(objItem_FlightBookingDetail);
                        }
                        objret = context.SaveChanges();
                        break;
                    case "plasmatechissueticketresdetail":
                        long gid =long.Parse(DateTime.Now.ToString("ssffff"));
                        foreach (var myobjectItem in myobject)
                        {
                            PlasmaTechIssueTicketResDetail objItem_FlightBookingDetail = new PlasmaTechIssueTicketResDetail();
                            PropertyCopier<TParent, PlasmaTechIssueTicketResDetail>.Copy(myobjectItem, objItem_FlightBookingDetail);
                            objItem_FlightBookingDetail.Id = 0;
                            objItem_FlightBookingDetail.Issued_Id = gid;
                            context.PlasmaTechIssueTicketResDetails.Add(objItem_FlightBookingDetail);
                        }

                        objret = context.SaveChanges();
                        break;
                    case "datapackdetail":
                        foreach (var myobjectItem in myobject)
                        {
                            DataPackDetail objItem_DataPackDetail = new DataPackDetail();
                            PropertyCopier<TParent, DataPackDetail>.Copy(myobjectItem, objItem_DataPackDetail);
                            objItem_DataPackDetail.Id = 0;
                            context.DataPackDetails.Add(objItem_DataPackDetail);
                        }
                        objret = context.SaveChanges();
                        break;
                    default:
                    case "notification":
                        foreach (var myobjectItem in myobject)
                        {
                            Notification objItem_Notification = new Notification();
                            PropertyCopier<TParent, Notification>.Copy(myobjectItem, objItem_Notification);
                            objItem_Notification.Id = 0;
                            context.Notifications.Add(objItem_Notification);
                        }
                        objret = context.SaveChanges();
                        break;
                    case "notificationcampaignexceldata":
                        foreach (var myobjectItem in myobject)
                        {
                            NotificationCampaignExcelData objItem_Notificationexcel = new NotificationCampaignExcelData();
                            PropertyCopier<TParent, NotificationCampaignExcelData>.Copy(myobjectItem, objItem_Notificationexcel);
                            objItem_Notificationexcel.Id = 0;
                            context.NotificationCampaignExcelDatas.Add(objItem_Notificationexcel);
                        }
                        objret = context.SaveChanges();
                        break;
                }
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

                throw;
            }
            return objret;
        }
        private static TChild RepoRecord(string TableName, MyPayEntities context, TParent inobject, TChild outobject)
        {

            Hashtable HT = Common.CreateHasTable(inobject);
            HT["Take"] = 1;
            CommonHelpers obj = new CommonHelpers();
            switch (TableName)
            {
                case nameof(Common.StoreProcedures.sp_UserAuthorization_Get):
                    DataTable dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddUserAuthorization objAuthObject = (AddUserAuthorization)CommonEntityConverter.DataTableToRecord<AddUserAuthorization>(dt);
                        PropertyCopier<AddUserAuthorization, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_FlightBookingDetails_Get_plasma):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddFlightBookingDetails objAuthObject = (AddFlightBookingDetails)CommonEntityConverter.DataTableToRecord<AddFlightBookingDetails>(dt);
                        PropertyCopier<AddFlightBookingDetails, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }

                    break;

           

                case nameof(Common.StoreProcedures.sp_GetFlightSwitchSettings):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddApiFlightSwitchSettings objFlightSwitchObject = (AddApiFlightSwitchSettings)CommonEntityConverter.DataTableToRecord<AddApiFlightSwitchSettings>(dt);
                        PropertyCopier<AddApiFlightSwitchSettings, TChild>.Copy(objFlightSwitchObject, outobject);
                        return outobject;
                    }
                    break;

                case nameof(Common.StoreProcedures.sp_MerchantIPAddress_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddMerchantIPAddress objAuthObject = (AddMerchantIPAddress)CommonEntityConverter.DataTableToRecord<AddMerchantIPAddress>(dt);
                        PropertyCopier<AddMerchantIPAddress, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }


                    break;
                case nameof(Common.StoreProcedures.sp_RemittanceDashboard_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddRemittanceDashboard objAuthObject = (AddRemittanceDashboard)CommonEntityConverter.DataTableToRecord<AddRemittanceDashboard>(dt);
                        PropertyCopier<AddRemittanceDashboard, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_RemittanceIPAddress_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddRemittanceIPAddress objAuthObject = (AddRemittanceIPAddress)CommonEntityConverter.DataTableToRecord<AddRemittanceIPAddress>(dt);
                        PropertyCopier<AddRemittanceIPAddress, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_MerchantWithdrawalRequest_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddMerchantWithdrawalRequest objAuthObject = (AddMerchantWithdrawalRequest)CommonEntityConverter.DataTableToRecord<AddMerchantWithdrawalRequest>(dt);
                        PropertyCopier<AddMerchantWithdrawalRequest, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }

                    break;
                case nameof(Common.StoreProcedures.sp_CalculateBalance_From_Date):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddBalanceHistory objAuthObject = (AddBalanceHistory)CommonEntityConverter.DataTableToRecord<AddBalanceHistory>(dt);
                        PropertyCopier<AddBalanceHistory, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_ApiSettingsHistory_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddApiSettingsHistory objAuthObject = (AddApiSettingsHistory)CommonEntityConverter.DataTableToRecord<AddApiSettingsHistory>(dt);
                        PropertyCopier<AddApiSettingsHistory, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }

                    break;
                case nameof(Common.StoreProcedures.sp_Merchant_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddMerchant objAuthObject = (AddMerchant)CommonEntityConverter.DataTableToRecord<AddMerchant>(dt);
                        PropertyCopier<AddMerchant, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }

                    break;
                case nameof(Common.StoreProcedures.Usp_AgentUserList):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddAgent objAuthObject = (AddAgent)CommonEntityConverter.DataTableToRecord<AddAgent>(dt);
                        PropertyCopier<AddAgent, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_MerchantBankDetail_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddMerchantBankDetail objAuthObject = (AddMerchantBankDetail)CommonEntityConverter.DataTableToRecord<AddMerchantBankDetail>(dt);
                        PropertyCopier<AddMerchantBankDetail, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }

                    break;
                case nameof(Common.StoreProcedures.sp_BankList_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddBankList objAuthObject = (AddBankList)CommonEntityConverter.DataTableToRecord<AddBankList>(dt);
                        PropertyCopier<AddBankList, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }

                    break;
                case nameof(Common.StoreProcedures.sp_DataPackDetail_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddDataPackDetail objAuthObject = (AddDataPackDetail)CommonEntityConverter.DataTableToRecord<AddDataPackDetail>(dt);
                        PropertyCopier<AddDataPackDetail, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }

                    break;
                case nameof(Common.StoreProcedures.sp_UserInActiveRemarks_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddUserInActiveRemarks objAuthObject = (AddUserInActiveRemarks)CommonEntityConverter.DataTableToRecord<AddUserInActiveRemarks>(dt);
                        PropertyCopier<AddUserInActiveRemarks, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }

                    break;
                case nameof(Common.StoreProcedures.sp_InsuranceDetail_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddInsuranceDetail objAuthObject = (AddInsuranceDetail)CommonEntityConverter.DataTableToRecord<AddInsuranceDetail>(dt);
                        PropertyCopier<AddInsuranceDetail, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_NotificationCampaignIDs_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddNotificationCampaignIDs objAuthObject = (AddNotificationCampaignIDs)CommonEntityConverter.DataTableToRecord<AddNotificationCampaignIDs>(dt);
                        PropertyCopier<AddNotificationCampaignIDs, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_ExportData_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddExportData objAuthObject = (AddExportData)CommonEntityConverter.DataTableToRecord<AddExportData>(dt);
                        PropertyCopier<AddExportData, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_FlightBookingDetails_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddFlightBookingDetails objAuthObject = (AddFlightBookingDetails)CommonEntityConverter.DataTableToRecord<AddFlightBookingDetails>(dt);
                        PropertyCopier<AddFlightBookingDetails, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }

                    break;
                case nameof(Common.StoreProcedures.sp_FlightPassengersDetails_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddFlightPassengersDetails objAuthObject = (AddFlightPassengersDetails)CommonEntityConverter.DataTableToRecord<AddFlightPassengersDetails>(dt);
                        PropertyCopier<AddFlightPassengersDetails, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }

                    break;
                case nameof(Common.StoreProcedures.sp_Menus_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddMenu objAuthObject = (AddMenu)CommonEntityConverter.DataTableToRecord<AddMenu>(dt);
                        PropertyCopier<AddMenu, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_MenusAssign_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddRoleOfMenus objAuthObject = (AddRoleOfMenus)CommonEntityConverter.DataTableToRecord<AddRoleOfMenus>(dt);
                        PropertyCopier<AddRoleOfMenus, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_Role_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddRole objAuthObject = (AddRole)CommonEntityConverter.DataTableToRecord<AddRole>(dt);
                        PropertyCopier<AddRole, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_KYCRemarks_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddKYCRemarks objAuthObject = (AddKYCRemarks)CommonEntityConverter.DataTableToRecord<AddKYCRemarks>(dt);
                        PropertyCopier<AddKYCRemarks, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_TicketsCategory_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddTicketCategory objAuthObject = (AddTicketCategory)CommonEntityConverter.DataTableToRecord<AddTicketCategory>(dt);
                        PropertyCopier<AddTicketCategory, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_Tickets_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddTicket objAuthObject = (AddTicket)CommonEntityConverter.DataTableToRecord<AddTicket>(dt);
                        PropertyCopier<AddTicket, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_TicketsReply_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddTicketsReply objAuthObject = (AddTicketsReply)CommonEntityConverter.DataTableToRecord<AddTicketsReply>(dt);
                        PropertyCopier<AddTicketsReply, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_TicketImages_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddTicketImages objAuthObject = (AddTicketImages)CommonEntityConverter.DataTableToRecord<AddTicketImages>(dt);
                        PropertyCopier<AddTicketImages, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_TicketRecordDetail_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddTicket objAuthObject = (AddTicket)CommonEntityConverter.DataTableToRecord<AddTicket>(dt);
                        PropertyCopier<AddTicket, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_Notification_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddNotification objAuthObject = (AddNotification)CommonEntityConverter.DataTableToRecord<AddNotification>(dt);
                        PropertyCopier<AddNotification, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_RedeemPoints_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddRedeemPoints objAuthObject = (AddRedeemPoints)CommonEntityConverter.DataTableToRecord<AddRedeemPoints>(dt);
                        PropertyCopier<AddRedeemPoints, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_RewardPointTransactions_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddRewardPointTransactions objAuthObject = (AddRewardPointTransactions)CommonEntityConverter.DataTableToRecord<AddRewardPointTransactions>(dt);
                        PropertyCopier<AddRewardPointTransactions, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_UserBankDetail_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddUserBankDetail objAuthObject = (AddUserBankDetail)CommonEntityConverter.DataTableToRecord<AddUserBankDetail>(dt);
                        PropertyCopier<AddUserBankDetail, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_ProviderServiceCategoryList_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddProviderServiceCategoryList objAuthObject = (AddProviderServiceCategoryList)CommonEntityConverter.DataTableToRecord<AddProviderServiceCategoryList>(dt);
                        PropertyCopier<AddProviderServiceCategoryList, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_CommissionUpdateHistory_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddCommissionUpdateHistory objAuthObject = (AddCommissionUpdateHistory)CommonEntityConverter.DataTableToRecord<AddCommissionUpdateHistory>(dt);
                        PropertyCopier<AddCommissionUpdateHistory, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_OfferBanners_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddOfferBanners objAuthObject = (AddOfferBanners)CommonEntityConverter.DataTableToRecord<AddOfferBanners>(dt);
                        PropertyCopier<AddOfferBanners, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_BalanceHistory_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddBalanceHistory objAuthObject = (AddBalanceHistory)CommonEntityConverter.DataTableToRecord<AddBalanceHistory>(dt);
                        PropertyCopier<AddBalanceHistory, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_AdminDashboard_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddAdmin objAuthObject = (AddAdmin)CommonEntityConverter.DataTableToRecord<AddAdmin>(dt);
                        PropertyCopier<AddAdmin, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_MerchantDashboard_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddMerchantDashboard objAuthObject = (AddMerchantDashboard)CommonEntityConverter.DataTableToRecord<AddMerchantDashboard>(dt);
                        PropertyCopier<AddMerchantDashboard, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_Users_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddUser objAuthObject = (AddUser)CommonEntityConverter.DataTableToRecord<AddUser>(dt);
                        PropertyCopier<AddUser, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }

                    break;
                case nameof(Common.StoreProcedures.sp_Users_GetAdmin):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddUser objAuthObject = (AddUser)CommonEntityConverter.DataTableToRecord<AddUser>(dt);
                        PropertyCopier<AddUser, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }

                    break;
                case nameof(Common.StoreProcedures.sp_verification_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddVerification objAuthObject = (AddVerification)CommonEntityConverter.DataTableToRecord<AddVerification>(dt);
                        PropertyCopier<AddVerification, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_logs_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddLog objAuthObject = (AddLog)CommonEntityConverter.DataTableToRecord<AddLog>(dt);
                        PropertyCopier<AddLog, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;

                case nameof(Common.StoreProcedures.sp_AdminUser_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddAdmin objAuthObject = (AddAdmin)CommonEntityConverter.DataTableToRecord<AddAdmin>(dt);
                        PropertyCopier<AddAdmin, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_DepositOrders_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddDepositOrders objAuthObject = (AddDepositOrders)CommonEntityConverter.DataTableToRecord<AddDepositOrders>(dt);
                        PropertyCopier<AddDepositOrders, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_Settings_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddSettings objAuthObject = (AddSettings)CommonEntityConverter.DataTableToRecord<AddSettings>(dt);
                        PropertyCopier<AddSettings, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_Commission_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddCommission objAuthObject = (AddCommission)CommonEntityConverter.DataTableToRecord<AddCommission>(dt);
                        PropertyCopier<AddCommission, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_AddAirlineCommision):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddAirlineCommission objAuthObject = (AddAirlineCommission)CommonEntityConverter.DataTableToRecord<AddAirlineCommission>(dt);
                        PropertyCopier<AddAirlineCommission, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_AirlinesCommission):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddAirlineCommission objAuthObject = (AddAirlineCommission)CommonEntityConverter.DataTableToRecord<AddAirlineCommission>(dt);
                        PropertyCopier<AddAirlineCommission, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_WalletTransactions_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddTransaction objAuthObject = (AddTransaction)CommonEntityConverter.DataTableToRecord<AddTransaction>(dt);
                        objAuthObject.VendorTransactionId = String.Empty;// SET TO EMPTY AS NOT TO SHOWN ANYWHERE IN APP.
                        objAuthObject.ProviderName = ((VendorApi_CommonHelper.KhaltiAPIName)Convert.ToInt32(objAuthObject.Type)).ToString().ToLower().Replace("khalti_", " ").Replace("merchant_bank_load", "Wallet Load").Replace("_", " ").Trim().ToUpper();

                        PropertyCopier<AddTransaction, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_Occupation_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddOccupation objAuthObject = (AddOccupation)CommonEntityConverter.DataTableToRecord<AddOccupation>(dt);
                        PropertyCopier<AddOccupation, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_KYCStatusHistory_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddKYCStatusHistory objAuthObject = (AddKYCStatusHistory)CommonEntityConverter.DataTableToRecord<AddKYCStatusHistory>(dt);
                        PropertyCopier<AddKYCStatusHistory, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_VendorAPIRequest_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddVendor_API_Requests objAuthObject = (AddVendor_API_Requests)CommonEntityConverter.DataTableToRecord<AddVendor_API_Requests>(dt);
                        PropertyCopier<AddVendor_API_Requests, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                //case nameof(Common.StoreProcedures.sp_RemittanceAPIRequest_Get):
                //    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                //    if (dt != null && dt.Rows.Count > 0)
                //    {
                //        AddRemittance_API_Requests objAuthObject = (AddRemittance_API_Requests)CommonEntityConverter.DataTableToRecord<AddRemittance_API_Requests>(dt);
                //        PropertyCopier<AddRemittance_API_Requests, TChild>.Copy(objAuthObject, outobject);
                //        return outobject;
                //    }
                //    break;
                case nameof(Common.StoreProcedures.sp_RemittanceAPIRequest_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddRemittance_API_Requests_VendorType objAuthObject = (AddRemittance_API_Requests_VendorType)CommonEntityConverter.DataTableToRecord<AddRemittance_API_Requests_VendorType>(dt);
                        PropertyCopier<AddRemittance_API_Requests_VendorType, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_CalculateServiceChargeAndCashback_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddCalculateServiceChargeAndCashback objAuthObject = (AddCalculateServiceChargeAndCashback)CommonEntityConverter.DataTableToRecord<AddCalculateServiceChargeAndCashback>(dt);
                        PropertyCopier<AddCalculateServiceChargeAndCashback, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_ProviderLogoList_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddProviderLogoList objAuthObject = (AddProviderLogoList)CommonEntityConverter.DataTableToRecord<AddProviderLogoList>(dt);
                        PropertyCopier<AddProviderLogoList, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_RequestFund_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddRequestFund objAuthObject = (AddRequestFund)CommonEntityConverter.DataTableToRecord<AddRequestFund>(dt);
                        PropertyCopier<AddRequestFund, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_VotingCandidate_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddVotingCandidate objAuthObject = (AddVotingCandidate)CommonEntityConverter.DataTableToRecord<AddVotingCandidate>(dt);
                        PropertyCopier<AddVotingCandidate, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_VotingCompetition_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddVotingCompetition objAuthObject = (AddVotingCompetition)CommonEntityConverter.DataTableToRecord<AddVotingCompetition>(dt);
                        PropertyCopier<AddVotingCompetition, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_VotingPackages_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddVotingPackages objAuthObject = (AddVotingPackages)CommonEntityConverter.DataTableToRecord<AddVotingPackages>(dt);
                        PropertyCopier<AddVotingPackages, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_VotingList_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddVotingList objAuthObject = (AddVotingList)CommonEntityConverter.DataTableToRecord<AddVotingList>(dt);
                        PropertyCopier<AddVotingList, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_Marque_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddMarque objAuthObject = (AddMarque)CommonEntityConverter.DataTableToRecord<AddMarque>(dt);
                        PropertyCopier<AddMarque, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_Purpose_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddPurpose objAuthObject = (AddPurpose)CommonEntityConverter.DataTableToRecord<AddPurpose>(dt);
                        PropertyCopier<AddPurpose, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_TransactionLimit_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddTransactionLimit objAuthObject = (AddTransactionLimit)CommonEntityConverter.DataTableToRecord<AddTransactionLimit>(dt);
                        PropertyCopier<AddTransactionLimit, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_BankTransactions_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddBankTransactions objAuthObject = (AddBankTransactions)CommonEntityConverter.DataTableToRecord<AddBankTransactions>(dt);
                        PropertyCopier<AddBankTransactions, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_UsersDeviceRegistration_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        UsersDeviceRegistration objAuthObject = (UsersDeviceRegistration)CommonEntityConverter.DataTableToRecord<UsersDeviceRegistration>(dt);
                        PropertyCopier<UsersDeviceRegistration, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_UsersDeviceRegistration_Check):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddUsersDeviceRegistration objAuthObject = (AddUsersDeviceRegistration)CommonEntityConverter.DataTableToRecord<AddUsersDeviceRegistration>(dt);
                        PropertyCopier<AddUsersDeviceRegistration, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_WalletTransactions_Count):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddTransactionSumCount objAuthObject = (AddTransactionSumCount)CommonEntityConverter.DataTableToRecord<AddTransactionSumCount>(dt);
                        PropertyCopier<AddTransactionSumCount, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_DashboardChart_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddDashboardChart objAuthObject = (AddDashboardChart)CommonEntityConverter.DataTableToRecord<AddDashboardChart>(dt);
                        PropertyCopier<AddDashboardChart, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_ShareReferLink_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddShareReferLink objAuthObject = (AddShareReferLink)CommonEntityConverter.DataTableToRecord<AddShareReferLink>(dt);
                        PropertyCopier<AddShareReferLink, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_ReferEarnImage_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddReferEarnImage objAuthObject = (AddReferEarnImage)CommonEntityConverter.DataTableToRecord<AddReferEarnImage>(dt);
                        PropertyCopier<AddReferEarnImage, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_NotificationCampaign_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddNotificationCampaign objAuthObject = (AddNotificationCampaign)CommonEntityConverter.DataTableToRecord<AddNotificationCampaign>(dt);
                        PropertyCopier<AddNotificationCampaign, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_MerchantOrders_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddMerchantOrders objAuthObject = (AddMerchantOrders)CommonEntityConverter.DataTableToRecord<AddMerchantOrders>(dt);
                        PropertyCopier<AddMerchantOrders, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_CouponsScratched_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddCouponsScratched objAuthObject = (AddCouponsScratched)CommonEntityConverter.DataTableToRecord<AddCouponsScratched>(dt);
                        PropertyCopier<AddCouponsScratched, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_Coupons_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddCoupons objAuthObject = (AddCoupons)CommonEntityConverter.DataTableToRecord<AddCoupons>(dt);
                        PropertyCopier<AddCoupons, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_Users_GetByPhoneNumber):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddUserByPhone objAuthObject = (AddUserByPhone)CommonEntityConverter.DataTableToRecord<AddUserByPhone>(dt);
                        PropertyCopier<AddUserByPhone, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_Users_GetLoginWithPin):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddUserLoginWithPin objAuthObject = (AddUserLoginWithPin)CommonEntityConverter.DataTableToRecord<AddUserLoginWithPin>(dt);
                        PropertyCopier<AddUserLoginWithPin, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_NotificationCampaignExcel_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddNotificationCampaignExcel objAuthObject = (AddNotificationCampaignExcel)CommonEntityConverter.DataTableToRecord<AddNotificationCampaignExcel>(dt);
                        PropertyCopier<AddNotificationCampaignExcel, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_ExcelNotificationCampaignIDs_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddExcelNotificationCampaignIDs objAuthObject = (AddExcelNotificationCampaignIDs)CommonEntityConverter.DataTableToRecord<AddExcelNotificationCampaignIDs>(dt);
                        PropertyCopier<AddExcelNotificationCampaignIDs, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_EstatementPDFToken_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddEstatementPDFToken objAuthObject = (AddEstatementPDFToken)CommonEntityConverter.DataTableToRecord<AddEstatementPDFToken>(dt);
                        PropertyCopier<AddEstatementPDFToken, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_DealsAndOffers_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddDealsandOffers objAuthObject = (AddDealsandOffers)CommonEntityConverter.DataTableToRecord<AddDealsandOffers>(dt);
                        PropertyCopier<AddDealsandOffers, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_CouponsHistory_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddCouponsHistory objAuthObject = (AddCouponsHistory)CommonEntityConverter.DataTableToRecord<AddCouponsHistory>(dt);
                        PropertyCopier<AddCouponsHistory, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }
                    break;

                case nameof(Common.StoreProcedures.UserDetail_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    { 
                        AddUserLoginWithPin objAuthObject = (AddUserLoginWithPin)CommonEntityConverter.DataTableToRecord<AddUserLoginWithPin>(dt);
                        PropertyCopier<AddUserLoginWithPin, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }

                    break;
                case nameof(Common.StoreProcedures.sp_BalanceHistoryMerchant_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddBalanceHistoryMerchant objAuthObject = (AddBalanceHistoryMerchant)CommonEntityConverter.DataTableToRecord<AddBalanceHistoryMerchant>(dt);
                        PropertyCopier<AddBalanceHistoryMerchant, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }

                    break;
                case nameof(Common.StoreProcedures.sp_CalculateBalanceMerchant_From_Date):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AddBalanceHistoryMerchant objAuthObject = (AddBalanceHistoryMerchant)CommonEntityConverter.DataTableToRecord<AddBalanceHistoryMerchant>(dt);
                        PropertyCopier<AddBalanceHistoryMerchant, TChild>.Copy(objAuthObject, outobject);
                        return outobject;
                    }

                    break;

                default:

                    break;
            }
            return outobject;
        }

        private static List<TChild> RepoRecordList(string TableName, MyPayEntities context, TParent inobject, TChild outobject)
        {
            DataTable dt;
            Hashtable HT = Common.CreateHasTable(inobject);
            CommonHelpers obj = new CommonHelpers();
            List<TChild> outobject_return = new List<TChild>();
            switch (TableName)
            {
                case nameof(Common.StoreProcedures.sp_OfferBanners_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddOfferBanners> objAuthObject = (List<AddOfferBanners>)CommonEntityConverter.DataTableToList<AddOfferBanners>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddOfferBanners objParentObject = objAuthObject[i];
                                PropertyCopier<AddOfferBanners, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;

                case nameof(Common.StoreProcedures.sp_MerchantWithdrawalRequest_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddMerchantWithdrawalRequest> objAuthObject = (List<AddMerchantWithdrawalRequest>)CommonEntityConverter.DataTableToList<AddMerchantWithdrawalRequest>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddMerchantWithdrawalRequest objParentObject = objAuthObject[i];
                                PropertyCopier<AddMerchantWithdrawalRequest, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_ApiSettingsHistory_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddApiSettingsHistory> objAuthObject = (List<AddApiSettingsHistory>)CommonEntityConverter.DataTableToList<AddApiSettingsHistory>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddApiSettingsHistory objParentObject = objAuthObject[i];
                                PropertyCopier<AddApiSettingsHistory, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_MerchantBankDetail_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddMerchantBankDetail> objAuthObject = (List<AddMerchantBankDetail>)CommonEntityConverter.DataTableToList<AddMerchantBankDetail>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddMerchantBankDetail objParentObject = objAuthObject[i];
                                PropertyCopier<AddMerchantBankDetail, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_BankList_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddBankList> objAuthObject = (List<AddBankList>)CommonEntityConverter.DataTableToList<AddBankList>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddBankList objParentObject = objAuthObject[i];
                                PropertyCopier<AddBankList, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_Merchant_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddMerchant> objAuthObject = (List<AddMerchant>)CommonEntityConverter.DataTableToList<AddMerchant>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddMerchant objParentObject = objAuthObject[i];
                                PropertyCopier<AddMerchant, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_DataPackDetail_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddDataPackDetail> objAuthObject = (List<AddDataPackDetail>)CommonEntityConverter.DataTableToList<AddDataPackDetail>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddDataPackDetail objParentObject = objAuthObject[i];
                                PropertyCopier<AddDataPackDetail, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_UserInActiveRemarks_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddUserInActiveRemarks> objAuthObject = (List<AddUserInActiveRemarks>)CommonEntityConverter.DataTableToList<AddUserInActiveRemarks>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddUserInActiveRemarks objParentObject = objAuthObject[i];
                                PropertyCopier<AddUserInActiveRemarks, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_Country_Get):

                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddCountry> objAuthObject = (List<AddCountry>)CommonEntityConverter.DataTableToList<AddCountry>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddCountry objParentObject = objAuthObject[i];
                                PropertyCopier<AddCountry, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;

                case nameof(Common.StoreProcedures.sp_BalanceHistory_Get):

                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddBalanceHistory> objAuthObject = (List<AddBalanceHistory>)CommonEntityConverter.DataTableToList<AddBalanceHistory>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddBalanceHistory objParentObject = objAuthObject[i];
                                PropertyCopier<AddBalanceHistory, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_BalanceHistoryMerchant_Get):

                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddBalanceHistoryMerchant> objAuthObject = (List<AddBalanceHistoryMerchant>)CommonEntityConverter.DataTableToList<AddBalanceHistoryMerchant>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddBalanceHistoryMerchant objParentObject = objAuthObject[i];
                                PropertyCopier<AddBalanceHistoryMerchant, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_FlightPassengersDetails_Get):

                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddFlightPassengersDetails> objAuthObject = (List<AddFlightPassengersDetails>)CommonEntityConverter.DataTableToList<AddFlightPassengersDetails>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddFlightPassengersDetails objParentObject = objAuthObject[i];
                                PropertyCopier<AddFlightPassengersDetails, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_GetFlightPassengersDetails):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        List<AddFlightPassengersDetails> objAuthObject = (List<AddFlightPassengersDetails>)CommonEntityConverter.DataTableToList<AddFlightPassengersDetails>(dt);
                        for (int i = 0; i < objAuthObject.Count; i++)
                        {
                            AddFlightPassengersDetails objParentObject = objAuthObject[i];
                            PropertyCopier<AddFlightPassengersDetails, TChild>.Copy(objParentObject, outobject);

                            string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                            TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                            outobject_return.Add(duplicateItemVO);
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_FlightBookingDetails_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddFlightBookingDetails> objAuthObject = (List<AddFlightBookingDetails>)CommonEntityConverter.DataTableToList<AddFlightBookingDetails>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddFlightBookingDetails objParentObject = objAuthObject[i];
                                PropertyCopier<AddFlightBookingDetails, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;

              
                case nameof(Common.StoreProcedures.sp_Menus_Get):

                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddMenu> objAuthObject = (List<AddMenu>)CommonEntityConverter.DataTableToList<AddMenu>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddMenu objParentObject = objAuthObject[i];
                                PropertyCopier<AddMenu, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_MenusAssign_Get):

                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddRoleOfMenus> objAuthObject = (List<AddRoleOfMenus>)CommonEntityConverter.DataTableToList<AddRoleOfMenus>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddRoleOfMenus objParentObject = objAuthObject[i];
                                PropertyCopier<AddRoleOfMenus, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_Role_Get):

                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddRole> objAuthObject = (List<AddRole>)CommonEntityConverter.DataTableToList<AddRole>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddRole objParentObject = objAuthObject[i];
                                PropertyCopier<AddRole, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_KYCRemarks_Get):

                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddKYCRemarks> objAuthObject = (List<AddKYCRemarks>)CommonEntityConverter.DataTableToList<AddKYCRemarks>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddKYCRemarks objParentObject = objAuthObject[i];
                                PropertyCopier<AddKYCRemarks, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_Tickets_Get):

                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddTicket> objAuthObject = (List<AddTicket>)CommonEntityConverter.DataTableToList<AddTicket>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddTicket objParentObject = objAuthObject[i];
                                PropertyCopier<AddTicket, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_DashboardChart_Get):

                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddDashboardChart> objAuthObject = (List<AddDashboardChart>)CommonEntityConverter.DataTableToList<AddDashboardChart>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddDashboardChart objParentObject = objAuthObject[i];
                                PropertyCopier<AddDashboardChart, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_TicketsCategory_Get):

                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddTicketCategory> objAuthObject = (List<AddTicketCategory>)CommonEntityConverter.DataTableToList<AddTicketCategory>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddTicketCategory objParentObject = objAuthObject[i];
                                PropertyCopier<AddTicketCategory, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_TicketsReply_Get):

                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddTicketsReply> objAuthObject = (List<AddTicketsReply>)CommonEntityConverter.DataTableToList<AddTicketsReply>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddTicketsReply objParentObject = objAuthObject[i];
                                PropertyCopier<AddTicketsReply, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_TicketImages_Get):

                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddTicketImages> objAuthObject = (List<AddTicketImages>)CommonEntityConverter.DataTableToList<AddTicketImages>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddTicketImages objParentObject = objAuthObject[i];
                                PropertyCopier<AddTicketImages, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_Notification_Get):

                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddNotification> objAuthObject = (List<AddNotification>)CommonEntityConverter.DataTableToList<AddNotification>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddNotification objParentObject = objAuthObject[i];
                                PropertyCopier<AddNotification, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_UsersDeviceRegistration_Get):

                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddUsersDeviceRegistration> objAuthObject = (List<AddUsersDeviceRegistration>)CommonEntityConverter.DataTableToList<AddUsersDeviceRegistration>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddUsersDeviceRegistration objParentObject = objAuthObject[i];
                                PropertyCopier<AddUsersDeviceRegistration, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_RedeemPoints_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddRedeemPoints> objAuthObject = (List<AddRedeemPoints>)CommonEntityConverter.DataTableToList<AddRedeemPoints>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddRedeemPoints objParentObject = objAuthObject[i];
                                PropertyCopier<AddRedeemPoints, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_RewardPointTransactions_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddRewardPointTransactions> objAuthObject = (List<AddRewardPointTransactions>)CommonEntityConverter.DataTableToList<AddRewardPointTransactions>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddRewardPointTransactions objParentObject = objAuthObject[i];
                                PropertyCopier<AddRewardPointTransactions, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_UserBankDetail_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddUserBankDetail> objAuthObject = (List<AddUserBankDetail>)CommonEntityConverter.DataTableToList<AddUserBankDetail>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddUserBankDetail objParentObject = objAuthObject[i];
                                PropertyCopier<AddUserBankDetail, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_Users_Get_all):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<AddUser> objAuthObject = (List<AddUser>)CommonEntityConverter.DataTableToList<AddUser>(dt);
                        for (int i = 0; i < objAuthObject.Count; i++)
                        {
                            AddUser objParentObject = objAuthObject[i];
                            PropertyCopier<AddUser, TChild>.Copy(objParentObject, outobject);
                            TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(Newtonsoft.Json.JsonConvert.SerializeObject(outobject));
                            outobject_return.Add(duplicateItemVO);
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_Occupation_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<AddOccupation> objAuthObject = (List<AddOccupation>)CommonEntityConverter.DataTableToList<AddOccupation>(dt);
                        for (int i = 0; i < objAuthObject.Count; i++)
                        {
                            AddOccupation objParentObject = objAuthObject[i];
                            PropertyCopier<AddOccupation, TChild>.Copy(objParentObject, outobject);

                            string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                            TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                            outobject_return.Add(duplicateItemVO);
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_State_Get):
                    dt = obj.GetDataFromStoredProcedure(TableName, HT);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<AddState> objAuthObject = (List<AddState>)CommonEntityConverter.DataTableToList<AddState>(dt);
                        for (int i = 0; i < objAuthObject.Count; i++)
                        {
                            AddState objParentObject = objAuthObject[i];
                            PropertyCopier<AddState, TChild>.Copy(objParentObject, outobject);

                            string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                            TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                            outobject_return.Add(duplicateItemVO);
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_WalletTransactions_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddTransaction> objAuthObject = (List<AddTransaction>)CommonEntityConverter.DataTableToList<AddTransaction>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddTransaction objParentObject = objAuthObject[i];
                                objParentObject.VendorTransactionId = String.Empty; // SET TO EMPTY AS NOT TO SHOWN ANYWHERE IN APP.
                                objParentObject.ProviderName = ((VendorApi_CommonHelper.KhaltiAPIName)Convert.ToInt32(objParentObject.Type)).ToString().Replace("khalti_", " ").Replace("_", " ").Trim().ToUpper();
                                PropertyCopier<AddTransaction, TChild>.Copy(objParentObject, outobject);
                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_DepositOrders_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddDepositOrders> objAuthObject = (List<AddDepositOrders>)CommonEntityConverter.DataTableToList<AddDepositOrders>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddDepositOrders objParentObject = objAuthObject[i];
                                PropertyCopier<AddDepositOrders, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_Settings_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddSettings> objAuthObject = (List<AddSettings>)CommonEntityConverter.DataTableToList<AddSettings>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddSettings objParentObject = objAuthObject[i];
                                PropertyCopier<AddSettings, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_Commission_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddCommission> objAuthObject = (List<AddCommission>)CommonEntityConverter.DataTableToList<AddCommission>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddCommission objParentObject = objAuthObject[i];
                                PropertyCopier<AddCommission, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_Province_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddState> objAuthObject = (List<AddState>)CommonEntityConverter.DataTableToList<AddState>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddState objParentObject = objAuthObject[i];
                                PropertyCopier<AddState, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_District_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddDistrict> objAuthObject = (List<AddDistrict>)CommonEntityConverter.DataTableToList<AddDistrict>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddDistrict objParentObject = objAuthObject[i];
                                PropertyCopier<AddDistrict, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_LocalLevel_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddLocalLevel> objAuthObject = (List<AddLocalLevel>)CommonEntityConverter.DataTableToList<AddLocalLevel>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddLocalLevel objParentObject = objAuthObject[i];
                                PropertyCopier<AddLocalLevel, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_Feedback_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddFeedback> objAuthObject = (List<AddFeedback>)CommonEntityConverter.DataTableToList<AddFeedback>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddFeedback objParentObject = objAuthObject[i];
                                PropertyCopier<AddFeedback, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_Users_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddUser> objAuthObject = (List<AddUser>)CommonEntityConverter.DataTableToList<AddUser>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddUser objParentObject = objAuthObject[i];
                                PropertyCopier<AddUser, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_KYCStatusHistory_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddKYCStatusHistory> objAuthObject = (List<AddKYCStatusHistory>)CommonEntityConverter.DataTableToList<AddKYCStatusHistory>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddKYCStatusHistory objParentObject = objAuthObject[i];
                                PropertyCopier<AddKYCStatusHistory, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_ProviderLogoList_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddProviderLogoList> objAuthObject = (List<AddProviderLogoList>)CommonEntityConverter.DataTableToList<AddProviderLogoList>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddProviderLogoList objParentObject = objAuthObject[i];
                                PropertyCopier<AddProviderLogoList, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_ProviderServiceCategoryList_Get):

                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddProviderServiceCategoryList> objAuthObject = (List<AddProviderServiceCategoryList>)CommonEntityConverter.DataTableToList<AddProviderServiceCategoryList>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddProviderServiceCategoryList objParentObject = objAuthObject[i];
                                PropertyCopier<AddProviderServiceCategoryList, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_AirlineList_Get):
                    
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddAirlinesList> objAuthObject = (List<AddAirlinesList>)CommonEntityConverter.DataTableToList<AddAirlinesList>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddAirlinesList objParentObject = objAuthObject[i];
                                PropertyCopier<AddAirlinesList, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_SectorList_Get):

                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddSectorList> objAuthObject = (List<AddSectorList>)CommonEntityConverter.DataTableToList<AddSectorList>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddSectorList objParentObject = objAuthObject[i];
                                PropertyCopier<AddSectorList, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_RequestFund_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddRequestFund> objAuthObject = (List<AddRequestFund>)CommonEntityConverter.DataTableToList<AddRequestFund>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddRequestFund objParentObject = objAuthObject[i];
                                PropertyCopier<AddRequestFund, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_VotingCandidate_Get):

                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddVotingCandidate> objAuthObject = (List<AddVotingCandidate>)CommonEntityConverter.DataTableToList<AddVotingCandidate>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddVotingCandidate objParentObject = objAuthObject[i];
                                PropertyCopier<AddVotingCandidate, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_VotingCompetition_Get):

                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddVotingCompetition> objAuthObject = (List<AddVotingCompetition>)CommonEntityConverter.DataTableToList<AddVotingCompetition>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddVotingCompetition objParentObject = objAuthObject[i];
                                PropertyCopier<AddVotingCompetition, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_VotingPackages_Get):

                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddVotingPackages> objAuthObject = (List<AddVotingPackages>)CommonEntityConverter.DataTableToList<AddVotingPackages>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddVotingPackages objParentObject = objAuthObject[i];
                                PropertyCopier<AddVotingPackages, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_VotingList_Get):

                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddVotingList> objAuthObject = (List<AddVotingList>)CommonEntityConverter.DataTableToList<AddVotingList>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddVotingList objParentObject = objAuthObject[i];
                                PropertyCopier<AddVotingList, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_Marque_Get):

                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddMarque> objAuthObject = (List<AddMarque>)CommonEntityConverter.DataTableToList<AddMarque>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddMarque objParentObject = objAuthObject[i];
                                PropertyCopier<AddMarque, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_Purpose_Get):

                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddPurpose> objAuthObject = (List<AddPurpose>)CommonEntityConverter.DataTableToList<AddPurpose>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddPurpose objParentObject = objAuthObject[i];
                                PropertyCopier<AddPurpose, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_BankTransactions_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddBankTransactions> objAuthObject = (List<AddBankTransactions>)CommonEntityConverter.DataTableToList<AddBankTransactions>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddBankTransactions objParentObject = objAuthObject[i];
                                PropertyCopier<AddBankTransactions, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_ReferEarnImage_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddReferEarnImage> objAuthObject = (List<AddReferEarnImage>)CommonEntityConverter.DataTableToList<AddReferEarnImage>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddReferEarnImage objParentObject = objAuthObject[i];
                                PropertyCopier<AddReferEarnImage, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_NotificationCampaign_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddNotificationCampaign> objAuthObject = (List<AddNotificationCampaign>)CommonEntityConverter.DataTableToList<AddNotificationCampaign>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddNotificationCampaign objParentObject = objAuthObject[i];
                                PropertyCopier<AddNotificationCampaign, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_Coupons_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddCoupons> objAuthObject = (List<AddCoupons>)CommonEntityConverter.DataTableToList<AddCoupons>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddCoupons objParentObject = objAuthObject[i];
                                PropertyCopier<AddCoupons, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_CouponsScratched_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddCouponsScratched> objAuthObject = (List<AddCouponsScratched>)CommonEntityConverter.DataTableToList<AddCouponsScratched>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddCouponsScratched objParentObject = objAuthObject[i];
                                PropertyCopier<AddCouponsScratched, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_NotificationCampaignExcel_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddNotificationCampaignExcel> objAuthObject = (List<AddNotificationCampaignExcel>)CommonEntityConverter.DataTableToList<AddNotificationCampaignExcel>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddNotificationCampaignExcel objParentObject = objAuthObject[i];
                                PropertyCopier<AddNotificationCampaignExcel, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_DealsAndOffers_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddDealsandOffers> objAuthObject = (List<AddDealsandOffers>)CommonEntityConverter.DataTableToList<AddDealsandOffers>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddDealsandOffers objParentObject = objAuthObject[i];
                                PropertyCopier<AddDealsandOffers, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_APIDealsandOffers_Get):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddDealsandOffers> objAuthObject = (List<AddDealsandOffers>)CommonEntityConverter.DataTableToList<AddDealsandOffers>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddDealsandOffers objParentObject = objAuthObject[i];
                                PropertyCopier<AddDealsandOffers, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                case nameof(Common.StoreProcedures.sp_Get_AgentBankList):
                    {
                        dt = obj.GetDataFromStoredProcedure(TableName, HT);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            List<AddBankList> objAuthObject = (List<AddBankList>)CommonEntityConverter.DataTableToList<AddBankList>(dt);
                            for (int i = 0; i < objAuthObject.Count; i++)
                            {
                                AddBankList objParentObject = objAuthObject[i];
                                PropertyCopier<AddBankList, TChild>.Copy(objParentObject, outobject);

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(outobject);
                                TChild duplicateItemVO = Newtonsoft.Json.JsonConvert.DeserializeObject<TChild>(json);
                                outobject_return.Add(duplicateItemVO);
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
            return outobject_return;
        }

    }
}