using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.NPS.Errors
{
    public class NCHLGenericError
    {
        public string error { get; set; }
        public string error_description { get; set; }

        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public object data { get; set; }
        public List<Object> classfielderrorlist { get; set; }

    }

    //public class NCHLFieldError
    //{
    //    public string field { get; set; }
    //    public string message { get; set; }
    //}

    }
