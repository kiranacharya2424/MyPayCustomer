using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Settings
{
    public class Res_Settings_Requests : CommonResponse
    {
        private string _Title = string.Empty;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        private string _AppMessage = string.Empty;
        public string AppMessage
        {
            get { return _AppMessage; }
            set { _AppMessage = value; }
        }
        private string _URL = string.Empty;
        public string URL
        {
            get { return _URL; }
            set { _URL = value; }
        }
    } 
}