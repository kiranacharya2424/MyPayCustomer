
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyPay.API.Models.Request.CableCar
{
   

    public class CableCarTicketTypes : CommonProp
    {

        public IEnumerable<CableCarTicketTypesData> GetTicketTyeps { get; set; }
        public CableCarTicketTypesData[] Property1 { get; set; }
    }

    public class CableCarTicketTypesData
    {
        public string TripType { get; set; }
        public string PassengerType { get; set; }
        public float Price { get; set; }
        public float BaseRate { get; set; }
        public float Vat { get; set; }
        public float Lc { get; set; }
        public string PassengerTypeDesc { get; set; }
     
    }

}