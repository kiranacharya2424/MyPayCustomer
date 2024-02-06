using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.SIMTV
{
    public class Res_Vendor_API_SIMTV_Lookup_Requests : CommonResponse
    {

        // SIMTV_data
        private SIMTV_Data _SIMTV_data = new SIMTV_Data();
        public SIMTV_Data SIMTV_data
        {

            get { return _SIMTV_data; }

            set { _SIMTV_data = value; }
        }
    }
    public class SIMTV_Data
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