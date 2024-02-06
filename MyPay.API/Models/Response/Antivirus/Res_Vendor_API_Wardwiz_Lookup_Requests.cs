using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Antivirus.Kaspersky
{
    public class Res_Vendor_API_Wardwiz_Lookup_Requests : CommonResponse
    {
        // Kaspersky_data
        private List<Wardwiz_Data> _Wardwiz_bills = new List<Wardwiz_Data>();
        public  List<Wardwiz_Data> Wardwiz_Bills
        {

            get { return _Wardwiz_bills; }

            set { _Wardwiz_bills = value; }
        }
    }
    public class Wardwiz_Data
    {
        // name
        private string _Idx = string.Empty;
        public string Idx
        {
            get { return _Idx; }
            set { _Idx = value; }
        }
        private string _Name = string.Empty;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Value = string.Empty;
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        private string _Amount = string.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }  
    }
}