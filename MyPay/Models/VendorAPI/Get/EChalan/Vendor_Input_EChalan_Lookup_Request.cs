using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class Vendor_Input_EChalan_Lookup_Request
    {

        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
         
        private string _reference = string.Empty;
        public string reference
        {
            get { return _reference; }
            set { _reference = value; }
        }

        // app_id
        private string _app_id = string.Empty;
        public string app_id
        {
            get { return _app_id; }
            set { _app_id = value; }
        }
        // voucher_no 
        private string _voucher_no = string.Empty;
        public string voucher_no
        {
            get { return _voucher_no; }
            set { _voucher_no = value; }
        }
        // service 
        private string _service = string.Empty;
        public string service
        {
            get { return _service; }
            set { _service = value; }
        }
        // fiscal_year 
        private string _fiscal_year = string.Empty;
        public string fiscal_year
        {
            get { return _fiscal_year; }
            set { _fiscal_year = value; }
        }

        private string _province_code = String.Empty;
        public string province_code
        {
            get { return _province_code; }
            set { _province_code = value; }
        }
        private string _district_code = String.Empty;
        public string district_code
        {
            get { return _district_code; }
            set { _district_code = value; }
        }
    }
}