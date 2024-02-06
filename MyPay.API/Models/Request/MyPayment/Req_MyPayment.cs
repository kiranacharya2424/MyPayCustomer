using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace MyPay.API.Models.Request.MyPayment
{
    public class Req_MyPayment : CommonProp
    {
        public int Id { get; set; }

        public string JsonData { get; set; }

        //ProviderName
        public string ProviderName {  get; set; }

        //ProviderTypeId
       public string ProviderTypeId {get; set; }
       public string memberID {get; set; }



    }
}