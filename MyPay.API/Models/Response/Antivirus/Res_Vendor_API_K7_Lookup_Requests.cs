using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response.Antivirus
{
    public class Res_Vendor_API_K7_Lookup_Requests : CommonResponse
    {
        // Kaspersky_data
        private List<K7_Data> _K7_bills = new List<K7_Data>();
        public List<K7_Data> K7_bills
        {

            get { return _K7_bills; }

            set { _K7_bills = value; }
        }
    }

    public class K7_Data
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