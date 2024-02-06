using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVendor_API_Requests : CommonGet
    {
      
        private Int32 _VendorType = 0;
        public Int32 VendorType
        {
            get { return _VendorType; }
            set { _VendorType = value; }
        }
        private string _Req_Input = string.Empty;
        public string Req_Input
        {
            get { return _Req_Input; }
            set { _Req_Input = value; }
        }
        private string _Res_Output = string.Empty;
        public string Res_Output
        {
            get { return _Res_Output; }
            set { _Res_Output = value; }
        }
        private string _Req_Khalti_Input = string.Empty;
        public string Req_Khalti_Input
        {
            get { return _Req_Khalti_Input; }
            set { _Req_Khalti_Input = value; }
        }
        private string _Res_Khalti_Output = string.Empty;
        public string Res_Khalti_Output
        {
            get { return _Res_Khalti_Output; }
            set { _Res_Khalti_Output = value; }
        }
        private string _Req_URL = string.Empty;
        public string Req_URL
        {
            get { return _Req_URL; }
            set { _Req_URL = value; }
        }
        private string _Req_Token = string.Empty;
        public string Req_Token
        {
            get { return _Req_Token; }
            set { _Req_Token = value; }
        }
        private string _Req_ReferenceNo = string.Empty;
        public string Req_ReferenceNo
        {
            get { return _Req_ReferenceNo; }
            set { _Req_ReferenceNo = value; }
        }
        private string _Req_Khalti_ReferenceNo = string.Empty;
        public string Req_Khalti_ReferenceNo
        {
            get { return _Req_Khalti_ReferenceNo; }
            set { _Req_Khalti_ReferenceNo = value; }
        }
        private string _Req_Khalti_URL = string.Empty;
        public string Req_Khalti_URL
        {
            get { return _Req_Khalti_URL; }
            set { _Req_Khalti_URL = value; }
        }
        private string _Res_Khalti_Status = string.Empty;
        public string Res_Khalti_Status
        {
            get { return _Res_Khalti_Status; }
            set { _Res_Khalti_Status = value; }
        }
        private string _Res_Khalti_State = string.Empty;
        public string Res_Khalti_State
        {
            get { return _Res_Khalti_State; }
            set { _Res_Khalti_State = value; }
        }
        private string _Res_Khalti_Message = string.Empty;
        public string Res_Khalti_Message
        {
            get { return _Res_Khalti_Message; }
            set { _Res_Khalti_Message = value; }
        }
        private string _Res_Khalti_Id = string.Empty;
        public string Res_Khalti_Id
        {
            get { return _Res_Khalti_Id; }
            set { _Res_Khalti_Id = value; }
        }
        private string _TransactionUniqueId = string.Empty;
        public string TransactionUniqueId
        {
            get { return _TransactionUniqueId; }
            set { _TransactionUniqueId = value; }
        }

    }
}