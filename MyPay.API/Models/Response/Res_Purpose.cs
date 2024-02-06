using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response
{
    public class Res_Purpose : CommonResponse
    {

        //AddPurpose
        private List<AddPurpose> _data = new List<AddPurpose>();
        public List<AddPurpose> data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
}