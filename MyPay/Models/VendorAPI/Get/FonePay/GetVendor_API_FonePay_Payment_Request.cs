using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{ 
    public class GetVendor_API_FonePay_Payment_Request
    {
        public string acquirerCountryCode { get; set; }
        public string acquiringBin { get; set; }
        public string issuerBin { get; set; }
        public string amount { get; set; }
        public string businessApplicationId { get; set; }
        public FonePay_CardAcceptor cardAcceptor { get; set; }
        public string  localTransactionDateTime { get; set; }
        public string recipientPrimaryAccountNumber { get; set; }
        public string retrievalReferenceNumber { get; set; }
        public string secondaryId { get; set; }
        public string senderAccountNumber { get; set; }
        public string senderName { get; set; }
        public string senderMobileNumber { get; set; }
        //public string senderReference { get; set; }
        //public string systemsTraceAuditNumber { get; set; }
        public string transactionCurrencyCode { get; set; }
        public string merchantCategoryCode { get; set; }
        public string remarks1 { get; set; }
        public string ibftUserNamePwdEncrypt { get; set; }
    }

    public class FonePay_Address
    {

        //city
        private string _city = "";
        public string city
        {
            get { return _city; }
            set { _city = value; }
        }


        //country
        private string _country = "";
        public string country
        {
            get { return _country; }
            set { _country = value; }
        }

    }

    public class FonePay_CardAcceptor
    {


        //FonePay_Address
        private FonePay_Address _address = new FonePay_Address();
        public FonePay_Address address
        {
            get { return _address; }
            set { _address = value; }
        }

        //name
        private string _name = "";
        public string name
        {
            get { return _name; }
            set { _name = value; }
        } 
    }


}