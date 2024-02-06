using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.MAXTV
{
    public class Res_Vendor_API_MAXTV_Lookup_Requests : CommonResponse
    {
        // Amount
        private string _Amount = string.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        // Customer_Name
        private string _Customer_Name = string.Empty;
        public string Customer_Name
        {
            get { return _Customer_Name; }
            set { _Customer_Name = value; }
        }
        // Session_ID
        private string _Session_ID = string.Empty;
        public string Session_ID
        {
            get { return _Session_ID; }
            set { _Session_ID = value; }
        }
        // CID_STB_Smartcard
        private string _CID_STB_Smartcard = string.Empty;
        public string CID_STB_Smartcard
        {
            get { return _CID_STB_Smartcard; }
            set { _CID_STB_Smartcard = value; }
        } 
        // MAXTV_data
        private List<MAXTV_Data> _TVS = new List<MAXTV_Data>();
        public List<MAXTV_Data>  TVS
        {

            get { return _TVS; }

            set { _TVS = value; }
        }
    }
    public class MAXTV_Data
    {
        // STB_NO
        private string _STB_NO = String.Empty;
        public string STB_NO
        {
            get { return _STB_NO; }
            set { _STB_NO = value; }
        }
        // _SmartCard_No
        private string _SmartCard_No = String.Empty;
        public string SmartCard_No
        {
            get { return _SmartCard_No; }
            set { _SmartCard_No = value; }
        }

    }
}