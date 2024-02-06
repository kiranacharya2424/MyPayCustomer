using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Antivirus.Kaspersky
{
    public class Res_Vendor_API_Eset_Lookup_Requests : CommonResponse
    {
        // Kaspersky_data
        private List<Eset_Data> _Eset_bills = new List<Eset_Data>();
        public  List<Eset_Data> Eset_Bills
        {

            get { return _Eset_bills; }

            set { _Eset_bills = value; }
        }
    }
    public class Eset_Data
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