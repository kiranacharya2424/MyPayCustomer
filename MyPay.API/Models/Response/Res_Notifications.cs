using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Add;
using MyPay.Models.Get;
namespace MyPay.API.Models
{
    public class Res_Notifications : CommonResponse
    {
        // Notifications
        private List<AddNotificationCampaign> _Notifications = new List<AddNotificationCampaign>();
        public List<AddNotificationCampaign> Notifications
        {
            get { return _Notifications; }
            set { _Notifications = value; }
        } 
    }
}