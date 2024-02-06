using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.EChalan
{
    public class Res_Vendor_API_EChalan_Lookup_Fisacal_Requests : CommonResponse
    {
        // _YearCode
        private List<EChalan_Lookup_FisacalYear> _Year = new List<EChalan_Lookup_FisacalYear>();
        public List<EChalan_Lookup_FisacalYear> Year
        {
            get { return _Year; }
            set { _Year = value; }
        }

    }

    public class EChalan_Lookup_FisacalYear
    {
        // _YearCode
        private string _YearCode = string.Empty;
        public string YearCode
        {
            get { return _YearCode; }
            set { _YearCode = value; }
        }

        // _YearName
        private string _YearName = string.Empty;
        public string YearName
        {
            get { return _YearName; }
            set { _YearName = value; }
        }
    }
}