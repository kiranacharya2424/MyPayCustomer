using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.PRABHUTV
{
    public class Res_Vendor_API_PRABHUTV_Lookup_Requests : CommonResponse
    {
        // session_id
        private string _SessionID = string.Empty;
        public string SessionID
        {
            get { return _SessionID; }
            set { _SessionID = value; }
        }
        // customer_name
        private string _CustomerName = string.Empty;
        public string CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }
        // balance
        private string _BalanceAmount = string.Empty;
        public string BalanceAmount
        {
            get { return _BalanceAmount; }
            set { _BalanceAmount = value; }
        }
        // STB_Count
        private string _STB_Count = string.Empty;
        public string STB_Count
        {
            get { return _STB_Count; }
            set { _STB_Count = value; }
        }
        // CustomerID
        private string _CustomerID = string.Empty;
        public string CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }
        // PRABHUTV_data
        private List<PRABHUTV_Data> _PRABHUTV_data = new List<PRABHUTV_Data>();
        public  List<PRABHUTV_Data> PRABHUTV_data
        {

            get { return _PRABHUTV_data; }

            set { _PRABHUTV_data = value; }
        }
    }
    public class PRABHUTV_Data
    {
        // name
        private string _Product_Name = string.Empty;
        public string Product_Name
        {
            get { return _Product_Name; }
            set { _Product_Name = value; }
        }
        private string _Service_Start_Date = string.Empty;
        public string Service_Start_Date
        {
            get { return _Service_Start_Date; }
            set { _Service_Start_Date = value; }
        }
        private string _Expiry_Date = string.Empty;
        public string Expiry_Date
        {
            get { return _Expiry_Date; }
            set { _Expiry_Date = value; }
        }
        private string _Serial_Number = string.Empty;
        public string  Serial_Number
        {
            get { return _Serial_Number; }
            set { _Serial_Number = value; }
        }
        private string _MAC_VC_Number = string.Empty;
        public string MAC_VC_Number
        {
            get { return _MAC_VC_Number; }
            set { _MAC_VC_Number = value; }
        }
        private string _BillAmount = string.Empty;
        public string BillAmount
        {
            get { return _BillAmount; }
            set { _BillAmount = value; }
        }
    }
}