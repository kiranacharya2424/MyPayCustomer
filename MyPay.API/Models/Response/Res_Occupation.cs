using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Occupation
{
    public class Res_Occupation : CommonResponse
    {

        //AddOccupation
        private List<AddOccupation> _data = new List<AddOccupation>();
        public List<AddOccupation> data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
}