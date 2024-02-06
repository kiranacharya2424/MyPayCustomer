using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.NPS.Response.Consumer
{

    public class NCHLConsumerResp
    {
        //public string responseCode { get; set; }
        //public string responseDescription { get; set; }
        //public object billsPaymentDescription { get; set; }
        //public object billsPaymentResponseCode { get; set; }
        //public List<object> fieldErrors { get; set; }

        public string voucherNo { get; set; }
        public double amount { get; set; }
        public double serviceCharge { get; set; }
        public string organization { get; set; }
        public string paidTo { get; set; }
        public string employerId { get; set; }
        public string yearMonth { get; set; }
        public string employerName { get; set; }
        public string instructionId { get; set; }
        public string message { get; set; }
        public int reponseCode { get; set; }
        public bool status { get; set; }
        public string Description { get; set; }
        public string requestedBy { get; set; }
        public string TransactionUniqueId { get; set; }

        public string violator { get; set; }

        public string ebpNo { get; set; }

        public string submissionNo { get; set; }
        public string address { get; set; }
        public string passportNo { get; set; }
        public string companyName { get; set; }
        public string dob { get; set; }
        public string country { get; set; }
        public string employeeType { get; set; }

        public string lotNo { get; set; }


        //public string employerId { get; set; }

        //public string employerId { get; set; }
    }
}


/*
  "serviceCharge": "chargeAmount",
        "Violator": "Particulars",
        "EBPNo.": "appTxnId",
        "amount": "amount",
        "serviceName": "Traffic Fine",
        "applicationID": "GON-7-TVRS-1",
        "paidTo": "appId",
        "submissionNo": "refId",
        "paidFor": "remarks"
 */