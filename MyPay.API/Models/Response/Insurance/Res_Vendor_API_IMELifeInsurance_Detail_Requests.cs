using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.Response.Insurance
{
    public class Res_Vendor_API_ArhantLifeInsurance_Detail_Requests : CommonResponse
    {
        /* public string PolicyNo { get; set; }
         public string Name { get; set; }
         public string PremiumAmount { get; set; }
         public string FineAmount { get; set; }
         public string TotalFine { get; set; }
         public string RebateAmount { get; set; }
         public string Amount { get; set; }
         public string PaymentDate { get; set; }
         public string DueDate { get; set; }
         public string NextDueDate { get; set; }
         public string PolicyStatus { get; set; }
         public string Term { get; set; }
         public string MaturityDate { get; set; }
         public string PayMode { get; set; }
         public string ProductName { get; set; }
         public string Token { get; set; }
         public string UniqueIdGuid { get; set; }
         public string SessionId { get; set; }

         private IMELifeInsuranceErrorResponse _ErrorResponse = new IMELifeInsuranceErrorResponse();
         public IMELifeInsuranceErrorResponse ErrorResponse
         {
             get { return _ErrorResponse; }
             set { _ErrorResponse = value; }
         }*/
  

     
            public string request_id { get; set; }
            public int amount { get; set; }
            public Datas properties { get; set; }
            public int session_id { get; set; }
           
        
    }
    /*    public class IMELifeInsuranceErrorResponse
        {
            public bool Status { get; set; }
            public string ErrorCode { get; set; }
            public string Message { get; set; }
            public string Error { get; set; }
            public string Details { get; set; }
            public string NextDueDate { get; set; }
            public string State { get; set; }
        }*/
    public class Datas
    {
        public string policy_no { get; set; }
        public string customer_name { get; set; }
        public string address { get; set; }
        public string product_name { get; set; }
        public string premium { get; set; }
        public string late_fee { get; set; }
        public string waive_amount { get; set; }
        public string previous_excess_short { get; set; }
    }
}
