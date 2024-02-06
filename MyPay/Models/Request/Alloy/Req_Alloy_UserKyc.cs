using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request
{
    public class Req_Alloy_UserKyc
    {
        private string _provider = string.Empty;
        public string provider
        {
            get => _provider;
            set => _provider = value;
        } 
    } 
}