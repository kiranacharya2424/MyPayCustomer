using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    
    public class GetVendor_API_CIT_Category : CommonGet
    {
        private string _responseMessage = string.Empty;
        public string responseMessage
        {
            get { return _responseMessage; }
            set { _responseMessage = value; }
        }
        private string _Message = string.Empty;
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        private List<CIT_Category_Detail> _data = new List<CIT_Category_Detail>();
        public List<CIT_Category_Detail> data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
    public class CIT_Category_Detail { 
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