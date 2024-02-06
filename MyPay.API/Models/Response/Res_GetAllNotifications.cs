using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response
{
    public class Res_GetAllNotifications:CommonResponse
    {
        // Notifications
        private List<AddNotification> _data = new List<AddNotification>();
        public List<AddNotification> data
        {
            get { return _data; }
            set { _data = value; }
        }

        //UnreadCount
        private int _UnreadCount = 0;
        public int UnreadCount
        {
            get { return _UnreadCount; }
            set { _UnreadCount = value; }
        }
    }
}