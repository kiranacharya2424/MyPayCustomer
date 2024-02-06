using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Events
{
    public class Res_Vendor_API_Events_Ticket_Download_Requests : CommonResponse
    {

       
        // status
        private bool _success = false;
        public bool success
        {
            get { return _success; }
            set { _success = value; }
        }

        // Message
        private string _message = string.Empty;
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
        private List<String> _data = new List<string>();
        public List<string> data
        {
            get { return _data; }
            set { _data = value; }
        }



    }

    
   
}
