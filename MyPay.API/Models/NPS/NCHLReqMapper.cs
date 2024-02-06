using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
  
    public class NCHLReqMapper
    {
        public List<Service> services { get; set; }
    }

    public class Service
    {
        public int vendorAPIType { get; set; }
        public Mapping mapping { get; set; }
    }

    public class Mapping
    {
        public string applicationID { get; set; }
        public string refId { get; set; }
        public string amount { get; set; }
        public string freeText1 { get; set; }

        public string freeText4 { get; set; }
        public string freeText5 { get; set; }
        public string freeCode1 { get; set; }
        public string freeCode2 { get; set; }
        public string freeCode4 { get; set;}

        public string addenda4 { get; set; }
        public string addenda3 { get; set; }

        public string particulars { get; set; }
        public string appId { get; set; }

        public string Code { get; set; }
        public string BankName { get; set; }
        public string CardNumber { get; set; }
        public string Amount { get; set; }

        public string Name { get; set; }

        public string ServiceCharge { get; set; }

    }


}
