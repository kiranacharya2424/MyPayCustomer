using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Internet.Arrownet
{
    public class Res_Vendor_API_Arrownet_Lookup_Requests : CommonResponse
    {

        // full_name
        private string _Full_name = string.Empty;
        public string Full_name
        {
            get { return _Full_name; }
            set { _Full_name = value; }
        }
        // days_remaining
        private string _Days_Remaining = string.Empty;
        public string Days_Remaining
        {
            get { return _Days_Remaining; }
            set { _Days_Remaining = value; }
        }
        // current_plan
        private string _Current_Plan = string.Empty;
        public string Current_Plan
        {
            get { return _Current_Plan; }
            set { _Current_Plan = value; }
        }
        // has_due
        private string _Has_Due = string.Empty;
        public string Has_Due
        {
            get { return _Has_Due; }
            set { _Has_Due = value; }
        }
        // Token
        private string _Token = string.Empty;
        public string Token
        {
            get { return _Token; }
            set { _Token = value; }
        }
        // Arrownet_data
        private List<Arrownet_Data> _Plan_Details = new List<Arrownet_Data>();
        public  List<Arrownet_Data>  Plan_Details
        {

            get { return _Plan_Details; }

            set { _Plan_Details = value; }
        }
    }
    public class Arrownet_Data
    { 
        private string _Duration = string.Empty;
        public string Duration
        {
            get { return _Duration; }
            set { _Duration = value; }
        }
        private string _Amount = string.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
    }
}