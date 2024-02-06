using System;
using System.Collections.Generic;

namespace MyPay.API.Models.CIT
{
    public class Res_Vendor_API_CIT_Category_Requests : CommonResponse
    {
        public CIT_Category_Detail _Data { get; set; }
        private string responseDescription;
        public string ResponseDescription
        {
            get => responseDescription;
            set => responseDescription = value;
        }

        private bool _success;
        public bool  success
        {
            get => _success;
            set => _success = value;
        }
        private string responseCode;
        public string ResponseCode
        {
            get => responseCode;
            set => responseCode = value;
        }
        private List<CIT_Category_Detail> _data = new List<CIT_Category_Detail>();
        public List<CIT_Category_Detail> data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
        public class CIT_Category_Detail
    {
            // category
            private string _category = string.Empty;
            public string category
            {
                get { return _category; }
                set { _category = value; }
            }
            // code
            private string _code = string.Empty;
            public string code
            {
                get { return _code; }
                set { _code = value; }
            }
            // labelText
            private string _labelText = string.Empty;
            public string labelText
            {
                get { return _labelText; }
                set { _labelText = value; }
            }
            // logoUrl
            private string _logoUrl = string.Empty;
            public string logoUrl
            {
                get { return _logoUrl; }
                set { _logoUrl = value; }
            }


        }


    
}