using MyPay.Models.Request.WebRequest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request.WebRequest
{
    public class WebRequest_TV : WebCommonProp
    {
        private string _CasId = String.Empty;
        public string CasId
        {
            get { return _CasId; }
            set { _CasId = value; }
        }
        private string _CAS_ID = String.Empty;
        public string CAS_ID
        {
            get { return _CAS_ID; }
            set { _CAS_ID = value; }
        }
        private string _Amount = String.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        private string _MemberId = String.Empty;
        public string MemberId
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

        private string _CustomerId = String.Empty;
        public string CustomerId
        {
            get { return _CustomerId; }
            set { _CustomerId = value; }
        }

        private string _SessionId = String.Empty;
        public string SessionId
        {
            get { return _SessionId; }
            set { _SessionId = value; }
        }

        private string _PackageId = String.Empty;
        public string PackageId
        {
            get { return _PackageId; }
            set { _PackageId = value; }
        }
        private string _PackageType = String.Empty;
        public string PackageType
        {
            get { return _PackageType; }
            set { _PackageType = value; }
        }
        private string _Number = String.Empty;
        public string Number
        {
            get { return _Number; }
            set { _Number = value; }
        }
        private string _Package = String.Empty;
        public string Package
        {
            get { return _Package; }
            set { _Package = value; }
        }
        private string _UserName = String.Empty;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private string _CustomerName = String.Empty;
        public string CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }
        private string _ContactNumber = String.Empty;
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }
        private string _Mobile_Number_1 = String.Empty;
        public string Mobile_Number_1
        {
            get { return _Mobile_Number_1; }
            set { _Mobile_Number_1 = value; }
        }
        private string _STB_OR_CAS_ID = String.Empty;
        public string STB_OR_CAS_ID
        {
            get { return _STB_OR_CAS_ID; }
            set { _STB_OR_CAS_ID = value; }
        }
        private string _Old_Ward_Number = String.Empty;
        public string Old_Ward_Number
        {
            get { return _Old_Ward_Number; }
            set { _Old_Ward_Number = value; }
        }
    }
}