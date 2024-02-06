using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using MyPay.Models.Add;
using MyPay.Models.Common;
using MyPay.Models.Get;
using MyPay.Models.Miscellaneous;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;
using Newtonsoft.Json;
using MyPay.Repository;
using System.IO;
using System.Globalization;
using MyPay.API.Models;
using MyPay.API.Models.Response;
using log4net;
using System.Web.Configuration;

namespace MyPay.Repository
{
    public static class RepNotifications
    {
        private static ILog log = LogManager.GetLogger(typeof(RepNotifications));
        public static string GetApiNotifications(string searchtext, AddUserLoginWithPin resGetRecord, string Take, string Skip, string Type, ref List<AddNotificationCampaign> list)
        {
            string msg = string.Empty;
            if (string.IsNullOrEmpty(Take))
            {
                Take = "0";
            }
            if (string.IsNullOrEmpty(Skip))
            {
                Skip = "0";
            }
            if (string.IsNullOrEmpty(Type))
            {
                Type = "0";
            }
            AddNotificationCampaign outobjecttrans = new AddNotificationCampaign();
            GetNotificationCampaign inobjectTrans = new GetNotificationCampaign();
            inobjectTrans.Take = Convert.ToInt32(Take);
            inobjectTrans.Skip = Convert.ToInt32(Skip) * Convert.ToInt32(Take);
            inobjectTrans.CheckDelete = 0;
            inobjectTrans.CheckActive = 1;
            inobjectTrans.MemberId = resGetRecord.MemberId;
            inobjectTrans.ScheduleDateTime = DateTime.UtcNow.ToString();
            //*********************************************************************
            //********************* FOR PENDING NOTIFICATIONS *********************
            //*********************************************************************
            List<AddNotificationCampaign> list_Notification = RepCRUD<GetNotificationCampaign, AddNotificationCampaign>.GetRecordList(Common.StoreProcedures.sp_NotificationCampaign_Get, inobjectTrans, outobjecttrans);

            List<AddNotificationCampaign> listExcelNotification = new List<AddNotificationCampaign>();
            listExcelNotification = RepCRUD<GetNotificationCampaign, AddNotificationCampaign>.GetRecordList(Common.StoreProcedures.sp_NotificationCampaignExcel_Get, inobjectTrans, outobjecttrans);

            list = list_Notification.Concat(listExcelNotification).OrderBy(c => c.ScheduleDateTime).ToList();
            list = list.OrderByDescending(c => Convert.ToDateTime(c.ScheduleDateTime)).ToList();
            if (list.Count > 0)
            {
                string ImagePrefix = Common.LiveSiteUrl;
                if (Convert.ToString(WebConfigurationManager.AppSettings["IsProduction"]) == "1")
                {
                    ImagePrefix = (Common.LiveSiteUrl);
                }
                else
                {
                    ImagePrefix = (Common.TestSiteUrl);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    if (!string.IsNullOrEmpty(list[i].NotificationImage))
                    {
                        list[i].NotificationImage = (ImagePrefix + "Images/NotificationImages/" + list[i].NotificationImage.ToString());
                        list[i].CreatedDatedt = list[i].ScheduleDateTime.ToString("dd-MMM-yyyy hh:mm tt");
                    }
                }
                msg = Common.CommonMessage.success;
            }
            else
            {
                list.Clear();
                msg = Common.CommonMessage.Data_Not_Found;
            }
            return msg;
        }


        public static IEnumerable<AddNotificationCampaign> GetAllNotifications(GetNotificationCampaign inobject)
        {
            AddNotificationCampaign outobject = new AddNotificationCampaign();
            inobject.CheckDelete = 0;
            List<AddNotificationCampaign> list = RepCRUD<GetNotificationCampaign, AddNotificationCampaign>.GetRecordList(Models.Common.Common.StoreProcedures.sp_NotificationCampaign_Get, inobject, outobject);
            return list;
        }
    }
}