using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Get;
namespace MyPay.API.Models
{
    public class Res_Service_Providers : CommonResponse
    {
        // reference
        private List<GetService_Providers> _Providers = new List<GetService_Providers>();
        public List<GetService_Providers> Providers
        {
            get { return _Providers; }
            set { _Providers = value; }
        } 
    }
}