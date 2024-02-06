using Microsoft.Ajax.Utilities;
using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class CommonResponse
    {
        //Message
        private string _Message = string.Empty;
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        //responseMessage
        private string _responseMessage = string.Empty;
        public string responseMessage
        {
            get { return _responseMessage; }
            set { _responseMessage = value; }
        }
        //Details
        private string _Details = string.Empty;
        public string Details
        {
            get { return _Details; }
            set { _Details = value; }
        }


        //ReponseCode
        private int _ReponseCode = 0;
        public int ReponseCode
        {
            get { return _ReponseCode; }
            set { _ReponseCode = value; }
        }


        //status
        private bool _status = false;
        public bool status
        {
            get { return _status; }
            set { _status = value; }
        }

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
        //CouponCode
        private string _CouponCode = string.Empty;
        public string CouponCode
        {
            get { return _CouponCode; }
            set { _CouponCode = value; }
        }
        //IsCouponUnlocked
        private bool _IsCouponUnlocked = false;
        public bool IsCouponUnlocked
        {
            get { return _IsCouponUnlocked; }
            set { _IsCouponUnlocked = value; }
        }
        //TransactionUniqueID
        private string _TransactionUniqueId = "";
        public string TransactionUniqueId
        {
            get { return _TransactionUniqueId; }
            set { _TransactionUniqueId = value; }
        }
        public object Data { get;set; }

      

    }
}