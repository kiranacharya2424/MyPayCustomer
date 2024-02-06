using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.RemittanceAPI.Get
{
    public class GetRemittance_API_Requests : CommonGet
    {
        #region "Properties" 
        private String _Req_Input = string.Empty;
        public String Req_Input
        {
            get { return _Req_Input; }
            set { _Req_Input = value; }
        }
        private String _Res_Output = string.Empty;
        public String Res_Output
        {
            get { return _Res_Output; }
            set { _Res_Output = value; }
        }
        private String _Req_URL = string.Empty;
        public String Req_URL
        {
            get { return _Req_URL; }
            set { _Req_URL = value; }
        }
        private String _Req_ReferenceNo = string.Empty;
        public String Req_ReferenceNo
        {
            get { return _Req_ReferenceNo; }
            set { _Req_ReferenceNo = value; }
        }
        private String _TransactionUniqueId = string.Empty;
        public String TransactionUniqueId
        {
            get { return _TransactionUniqueId; }
            set { _TransactionUniqueId = value; }
        }
        private String _VendorTransactionId = string.Empty;
        public String VendorTransactionId
        {
            get { return _VendorTransactionId; }
            set { _VendorTransactionId = value; }
        }
        private String _MerchantUniqueId = string.Empty;
        public String MerchantUniqueId
        {
            get { return _MerchantUniqueId; }
            set { _MerchantUniqueId = value; }
        }
        private String _MerchantName = string.Empty;
        public String MerchantName
        {
            get { return _MerchantName; }
            set { _MerchantName = value; }
        }
        private String _OrganizationName = string.Empty;
        public String OrganizationName
        {
            get { return _OrganizationName; }
            set { _OrganizationName = value; }
        }
        private String _ContactNo = string.Empty;
        public String ContactNo
        {
            get { return _ContactNo; }
            set { _ContactNo = value; }
        }
        private String _Signature = string.Empty;
        public String Signature
        {
            get { return _Signature; }
            set { _Signature = value; }
        }
        private Int32 _Status = 0;
        public Int32 Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private String _Remarks = string.Empty;
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        private String _DeviceId = string.Empty;
        public String DeviceId
        {
            get { return _DeviceId; }
            set { _DeviceId = value; }
        }
        private String _IpAddress = Common.Common.GetUserIP();
        public String IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }
        private String _PlatForm = string.Empty;
        public String PlatForm
        {
            get { return _PlatForm; }
            set { _PlatForm = value; }
        }
        private Int32 _RemittanceApiType = 0;
        public Int32 RemittanceApiType
        {
            get { return _RemittanceApiType; }
            set { _RemittanceApiType = value; }
        }
        private String _CheckCreatedDate = string.Empty;
        public String CheckCreatedDate
        {
            get { return _CheckCreatedDate; }
            set { _CheckCreatedDate = value; }
        }
        private String _StartDate = string.Empty;
        public String StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        private String _EndDate = string.Empty;
        public String EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
       
        #endregion
    }
}