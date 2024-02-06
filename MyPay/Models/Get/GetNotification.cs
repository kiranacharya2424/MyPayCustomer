using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Get
{
    public class GetNotification : CommonGet
    {
        #region "Properties"

        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //_NotificationType
        private int _NotificationType = 0;
        public int NotificationType
        {
            get { return _NotificationType; }
            set { _NotificationType = value; }
        }

        //ReadStatus
        private int _ReadStatus =-1;
        public int ReadStatus
        {
            get { return _ReadStatus; }
            set { _ReadStatus = value; }
        }



        #endregion
    }
}