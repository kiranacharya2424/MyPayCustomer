using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_ChangePushNotificationStatus : CommonProp
    {
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //EnablePushNotification
        private string _EnablePushNotification = string.Empty;
        public string EnablePushNotification
        {
            get { return _EnablePushNotification; }
            set { _EnablePushNotification = value; }
        }
    }
}