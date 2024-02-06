using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response.Insurance
{
    public class Res_Vendor_API_NecoInsurance_Lookup_Requests:CommonResponse
    {
        // _NecoInsurance_categories 
        private List<string> _PolicyCategories;
        public List<string> PolicyCategories
        {
            get { return _PolicyCategories; }
            set { _PolicyCategories = value; }
        }
        private List<string> _Branches;
        public List<string> Branches
        {
            get { return _Branches; }
            set { _Branches = value; }
        }
    }

    public class NecoInsurance_category_list
    {
        //neco-insurance
        private string _neco_insurance = string.Empty;
        public string neco_insurance
        {
            get { return _neco_insurance; }
            set { _neco_insurance = value; }
        }
        
    }
}