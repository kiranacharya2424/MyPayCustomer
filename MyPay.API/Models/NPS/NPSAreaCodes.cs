using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.NPS
{
    public class Province
    {
        public string province_code { get; set; }
        public string province_name { get; set; }
        public string province_name_np { get; set; }
        public List<District> districts { get; set; }
    }

    public class District
    {
        public string district_code { get; set; }
        public string district_desc { get; set; }
        public string district_desc_np { get; set; }
    }

    public class NPSAreaCodes
    {
        public List<Province> data { get; set; }
    }

    //public class Datum
    //{
    //    public int id { get; set; }
    //    public string option { get; set; }
    //    public string value { get; set; }
    //    public object code { get; set; }
    //}

    public class FiscalYear
    {
        //public string responseCode { get; set; }
        //public string responseMessage { get; set; }
        public List<FiscalYearData> data { get; set; }
    }

    public class FiscalYearData
    {
        public int id { get; set; }
        public string option { get; set; }
        public string value { get; set; }
        public object code { get; set; }
    }

}
