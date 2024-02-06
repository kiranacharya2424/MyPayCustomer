using MyPay.Models.Request.WebRequest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request.WebRequest
{
    public class WebRequest_Pin:WebCommonProp
    {
        private string _Pin = string.Empty;
        public string Pin
        {
            get { return _Pin; }
            set { _Pin = value; }
        }

        private string _ConfirmPin = string.Empty;
        public string ConfirmPin
        {
            get { return _ConfirmPin; }
            set { _ConfirmPin = value; }
        }

        private string _Password = string.Empty;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        private bool _IsMobile = false;
        public bool IsMobile
        {
            get { return _IsMobile; }
            set { _IsMobile = value; }
        }
    }
}