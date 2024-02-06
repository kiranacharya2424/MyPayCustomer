using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.NPS
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Datum
    {
        public int id { get; set; }
        public string option { get; set; }
        public string value { get; set; }
        public string code { get; set; }
    }

    public class LoanTypes
    {
        public List<Datum> data { get; set; }
    }


}
