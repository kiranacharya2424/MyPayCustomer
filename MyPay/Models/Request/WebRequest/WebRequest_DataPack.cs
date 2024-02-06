using MyPay.Models.Request.WebRequest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request.WebRequest
{
    public class WebRequest_DataPack:WebCommonProp
    {
        // MemberId
        private string _MemberId = string.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        // Amount
        private string _Amount = string.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        // Number
        private string _Number = string.Empty;
        public string Number
        {
            get { return _Number; }
            set { _Number = value; }
        }
        // ProductCode
        private string _ProductCode = string.Empty;
        public string ProductCode
        {
            get { return _ProductCode; }
            set { _ProductCode = value; }
        }
        // ProductType
        private string _ProductType = string.Empty;
        public string ProductType
        {
            get { return _ProductType; }
            set { _ProductType = value; }
        }
        // PackageId
        private string _PackageId = string.Empty;
        public string PackageId
        {
            get { return _PackageId; }
            set { _PackageId = value; }
        }
        // reference
        private string _ReferenceNo = string.Empty;
        public string ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        // IsMobile
        private bool _IsMobile = false;
        public bool IsMobile
        {
            get { return _IsMobile; }
            set { _IsMobile = value; }
        }
    }
}