using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response.Antivirus
{
    public class Res_Vendor_API_Mcafee_Lookup_Requests : CommonResponse
    {
        private List<Mcafee_Data> _Mcafee_bills = new List<Mcafee_Data>();
        public List<Mcafee_Data> Mcafee_bills
        {

            get { return _Mcafee_bills; }

            set { _Mcafee_bills = value; }
        }
    }

    public class Mcafee_Data
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