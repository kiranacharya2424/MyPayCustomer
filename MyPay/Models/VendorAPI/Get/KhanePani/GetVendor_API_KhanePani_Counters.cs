using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get.KhanePani
{
    public class GetVendor_API_KhanePani_Counters : CommonGet
    {

        // status
        private bool _status = false;
        public bool status
        {
            get { return _status; }
            set { _status = value; }
        }

        // Message
        private string _Message = string.Empty;
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        // details
        private string _details = string.Empty;
        public string details
        {
            get { return _details; }
            set { _details = value; }
        }

        // counters
        private List<counters> _counters = new List<counters>();
        public List<counters> counters
        {
            get { return _counters; }
            set { _counters = value; }
        }
    }
    public class counters
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