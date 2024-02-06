using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.CLEARTV
{
    public class Res_Vendor_API_CLEARTV_Lookup_Requests : CommonResponse
    {

        // CLEARTV_data
        private CLEARTV_Data _CLEARTV_data = new CLEARTV_Data();
        public CLEARTV_Data CLEARTV_data
        {

            get { return _CLEARTV_data; }

            set { _CLEARTV_data = value; }
        }
    }
    public class CLEARTV_Data
    {
        // Due_Amount
        private string _Due_Amount = String.Empty;
        public string Due_Amount
        {
            get { return _Due_Amount; }
            set { _Due_Amount = value; }
        } 
        private string _Customer_Name = String.Empty;
        public string Customer_Name
        {
            get { return _Customer_Name; }
            set { _Customer_Name = value; }
        }

    }
}