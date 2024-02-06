using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.KhanePani
{
    public class Res_Vendor_API_Counters_KhanePani_Requests : CommonResponse
    {
    
        // state
        private string _state = string.Empty;
        public string state
        {
            get { return _state; }
            set { _state = value; }
        }
        // counters
        private List<service_group_counters> _counters = new List<service_group_counters>();
        public List<service_group_counters> counters
        {
            get { return _counters; }
            set { _counters = value; }
        }
    }
    public class service_group_counters
    {
        // name
        private string _name = String.Empty;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        // value
        private string _value = String.Empty;
        public string value
        {
            get { return _value; }
            set { _value = value; }
        }

    }
}