using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.Models
{
    public class OrganizationModel
    {

        public class CommonResponseDataOrganization
        {
            public bool status { get; set; }
            public string StatusCode { get; set; }
            public int ResponseCode { get; set; }
            public string Message { get; set; }
            public object Data { get; set; }

        }
        public class GetDataFromOrganization
        {
            public string message { get; set; }
            public string Id { get; set; }

        }

    }
}
