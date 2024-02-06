using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class CommonDBResponse
    {
        public string Message { get; set; }
        public string responseMessage { get; set; }
        public string Details { get; set; }
        public int ReponseCode { get; set; }
        public bool status { get; set; }
        //IosVersion
        private string _ios_version = Common.IosVersion;
        public string ios_version
        {
            get { return _ios_version; }
            set { _ios_version = value; }
        }

        //AndroidVersion
        private string _AndroidVersion = Common.AndroidVersion;
        public string AndroidVersion
        {
            get { return _AndroidVersion; }
            set { _AndroidVersion = value; }
        }

        public string CouponCode { get; set; }

        public bool IsCouponUnlocked { get; set; }
        public string TransactionUniqueId { get; set; }
        public object Data { get; set; }


    }
}