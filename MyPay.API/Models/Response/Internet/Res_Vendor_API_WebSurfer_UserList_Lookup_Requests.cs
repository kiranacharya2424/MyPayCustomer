using MyPay.Models.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Internet.WebSurfer
{
    public class Res_Vendor_API_WebSurfer_UserList_Lookup_Requests : CommonResponse
    {
        public Websurfer_Customer customer { get; set; }
        public List<Websurfer_Connection> connection { get; set; }
        public int SessionId { get; set; } 
    }
}