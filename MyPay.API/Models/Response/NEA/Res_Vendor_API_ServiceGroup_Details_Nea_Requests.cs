using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Res_Vendor_API_ServiceGroup_Details_Nea_Requests : CommonResponse
    {

        // session_id
        private string _Session_Id = string.Empty;
        public string Session_Id
        {
            get { return _Session_Id; }
            set { _Session_Id = value; }
        }
        // consumer_name
        private string _Consumer_Name = string.Empty;
        public string Consumer_Name
        {
            get { return _Consumer_Name; }
            set { _Consumer_Name = value; }
        }
        // PhoneNumber
        /*private string _PhoneNumber;
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }
        // Email
        private string _Email = string.Empty;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }*/
        // Total_Due_Amount
        private string _Total_Due_Amount = string.Empty;
        public string Total_Due_Amount
        {
            get { return _Total_Due_Amount; }
            set { _Total_Due_Amount = value; }
        }
        // Status
        private bool _Status = false;
        public bool Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        // counters
        private List<Consumer_Due_Bills> _Due_Bills = new List<Consumer_Due_Bills>();
        public List<Consumer_Due_Bills> Due_Bills
        {
            get { return _Due_Bills; }
            set { _Due_Bills = value; }
        }
    }
    public class Consumer_Due_Bills
    {
        // Bill_Amount
        private string _Bill_Amount = String.Empty;
        public string Bill_Amount
        {
            get { return _Bill_Amount; }
            set { _Bill_Amount = value; }
        }

        // Date
        private string _Bill_Date = String.Empty;
        public string Bill_Date
        {
            get { return _Bill_Date; }
            set { _Bill_Date = value; }
        }

        // Days
        private string _Days = String.Empty;
        public string Days
        {
            get { return _Days; }
            set { _Days = value; }
        }

        // Payable_Amount
        private string _Payable_Amount = String.Empty;
        public string Payable_Amount
        {
            get { return _Payable_Amount; }
            set { _Payable_Amount = value; }
        }
        // Due_Bill_Of
        private string _Due_Bill_Of = String.Empty;
        public string Due_Bill_Of
        {
            get { return _Due_Bill_Of; }
            set { _Due_Bill_Of = value; }
        }
        // Status
        private string _Status = String.Empty;
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }


       
    }
}