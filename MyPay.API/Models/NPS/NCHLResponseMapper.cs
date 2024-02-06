using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.NPS
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);


    public class Mapping
    {
        public string serviceName { get; set; }
        public string applicationID { get; set; }
        public string voucherNo { get; set; }
        public string amount { get; set; }
        public string serviceCharge { get; set; }
        public string organization { get; set; }
        public string requestedBy { get; set; }
        public string paidTo { get; set; }
        public string RMIS_EBP_Code { get; set; }
        public string submissionNo { get; set; }
        public string employerId { get; set; }
        public string yearMonth { get; set; }
        public string employerName { get; set; }
        public string paidFor { get; set; }
        public string ebpNo { get; set; }
        public string violator { get; set; }
        public string address { get; set; }
        public string particular { get; set; }
        public string passportNo { get; set; }
        public string creditorName { get; set; }
        public string companyName { get; set; }
        public string dob { get; set; }
        public string country { get; set; }
        public string employeeType { get; set; }
        public string lotNo { get; set; }

        public string cardNo { get; set; }
        public string cardType { get; set; }

    }

    public class NCHLResponseMapper

    {
        public List<Service> services { get; set; }
    }

    public class Service
    {
        public int vendorAPIType { get; set; }
        public Mapping mapping { get; set; }
    }




    //public class NCHLResponseMapper
    //{
    //    public List<Service> services { get; set; }
    //}

    //public class Service
    //{
    //    public int vendorAPIType { get; set; }
    //    public Mapping mapping { get; set; }
    //}

    //public class Mapping
    //{
    //    public string serviceName { get; set; }
    //    public string applicationID { get; set; }
    //    public string refId { get; set; }
    //    public string amount { get; set; }
    //    public string chargeAmount { get; set; }
    //    public string appId { get; set; }
    //    public string particulars { get; set; }
    //    public string remarks { get; set; }
    //    public string freeText1 { get; set; }
    //    public string addenda3 { get; set; }
    //    public string freeText2 { get; set; }
    //}

}
