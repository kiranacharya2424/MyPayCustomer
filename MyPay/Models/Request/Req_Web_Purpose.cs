using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request
{
    public class Req_Web_Purpose
    {
        private List<AddPurpose> _objData = new List<AddPurpose>();
        public List<AddPurpose> objData
        {
            get { return _objData; }
            set { _objData = value; }
        }
    }
}